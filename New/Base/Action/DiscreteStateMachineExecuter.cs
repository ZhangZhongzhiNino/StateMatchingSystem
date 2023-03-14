﻿using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
namespace Nino.NewStateMatching
{
    public class DiscreteStateMachineExecuter :  SMSExecuter
    {
        [FoldoutGroup("State Machine Setting")] public ItemSelector selectDefaultItem;
        [FoldoutGroup("State Machine Setting")] public bool enterStateWhenEnabled;
        [FoldoutGroup("State Machine Setting")] public bool entered;

        [FoldoutGroup("Other")] public SMSState currentState;
        [FoldoutGroup("Other")] public SMSupdater smsUpdater;
        [FoldoutGroup("Other")] public AddressData rootAddress;

        

        [FoldoutGroup("State Switcher")] public Dictionary<string, StateSwitchAction> dic_state;
        [FoldoutGroup("State Switcher/Add Trigger")] public ItemSelector triggerSelector;
        [FoldoutGroup("State Switcher/Add State")] public ItemSelector stateSelector;

        public override void InitializeAfterCreateAddress()
        {
            if(rootAddress == null) rootAddress = address.GetRootAddress();
            if(triggerSelector == null) triggerSelector = new ItemSelector(rootAddress, typeof(UnityEvent),group:"Trigger",groupedItem: true);
            if (stateSelector == null) stateSelector = new ItemSelector(address, typeof(SMSState), group: "State", groupedItem: true);
        }

        protected override void InitializeInstance()
        {
            if (selectDefaultItem == null) selectDefaultItem = new ItemSelector(address, typeof(SMSState), group: "State", groupedItem: true);
            if (smsUpdater == null)
            {
                smsUpdater = gameObject.AddComponent<SMSupdater>();
                smsUpdater.Initialize();
            }
            if (dic_state == null) dic_state = new Dictionary<string, StateSwitchAction>();

            dataController.AddItem(
                new LabledItem(
                    new SMSAction(
                        "Transist to State",
                        (object input) => TransistToState(input),
                        haveInput: true,
                        inputType: typeof(SMSState)),
                    "Transist to State",
                    "Action"
                ));
            dataController.AddItem(
                new LabledItem(
                    new SMSAction(
                        "EnterFSM",
                        (object input) => EnterFSM(input)),
                    "EnterFSM",
                    "Action"
                ));
            dataController.AddItem(
                new LabledItem(
                    new SMSAction(
                        "ExitFSM",
                        (object input) => ExitFSM(input)),
                    "ExitFSM",
                    "Action"
                ));
        }
        
        protected override void PreRemoveInstance()
        {
                
        }

        [FoldoutGroup("State Switcher/Add Trigger"), ShowIf("@triggerSelector.item != null")]
        [Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)]
        public void AddTrigger()
        {
            string triggerName = triggerSelector.item.itemName;
            if (triggerSelector.item.value is UnityEvent) 
                dic_state.TryAdd(triggerName,new StateSwitchAction(this,triggerSelector));
        }
        [FoldoutGroup("State Switcher/Add State")]
        [Button(ButtonHeight = 40, Style = ButtonStyle.Box), GUIColor(0.4f, 1, 0.4f),ShowIf("@stateSelector.item != null")]
        public void AddState([ValueDropdown("@dic_state.Keys")] string triggerName)
        {
            dic_state[triggerName].stateSelectors.Add( new ItemSelector(stateSelector));
        }

                
        private void OnEnable()
        {
            EnableStateSwitchActions();
            if (enterStateWhenEnabled) EnterFSM();
        }

        private void OnDisable()
        {
            DisableStateSwitchActions();
            ExitFSM();
        }
        public void TransistToState(System.Object nextState)
        {
            SMSState _nextState = (SMSState)(object)nextState;
            currentState.ExistState();
            _nextState.EnterState();
            currentState = _nextState;
        }
        public static void TransistToState(DiscreteStateMachineExecuter executer, System.Object nextState)
        {
            executer.TransistToState(nextState);
        }
        public void EnterFSM(object input = null)
        {
            entered = true;
            if (selectDefaultItem.item == null) return; 
            currentState = selectDefaultItem.item.value as SMSState;
            currentState.EnterState();
        }
        public void ExitFSM(object input = null)
        {
            currentState?.ExistState();
            entered = false;
        }

        void EnableStateSwitchActions()
        {
            if(dic_state == null) return;
            foreach (StateSwitchAction value in dic_state.Values)
            {
                value.Enable();
            }
        }
        void DisableStateSwitchActions()
        {
            if (dic_state == null) return;
            foreach (StateSwitchAction value in dic_state.Values)
            {
                value.Disable();
            }
        }
        [FoldoutGroup("Add & Edit State"), ShowInInspector,ListDrawerSettings(ListElementLabelName = "stateName")]
        public List<SMSState> states
        {
            get
            {
                List<LabledItem> getItems = dataController.labledItems.FindAll(x => x.group == "State");
                List<SMSState> r = new List<SMSState>();
                getItems.ForEach(x => r.Add(x.value as SMSState));
                return r;
            }
            set { }
        }
        [FoldoutGroup("Add & Edit State")]
        [Button(Style = ButtonStyle.Box),GUIColor(0.4f,1,0.4f)]
        public void AddNewState(string newStateName)
        {
            SMSState newState = new SMSState(newStateName, smsUpdater, rootAddress);
            LabledItem labledState = new LabledItem(newState, newStateName, "State");
            dataController.AddItem(labledState);
        }
        [Button]
        void dbug()
        {
            
        }
    }
    [System.Serializable]
    public class StateSwitchAction
    {
        [HideInInspector, OdinSerialize] public DiscreteStateMachineExecuter executer;
        [HideInInspector, OdinSerialize] public ItemSelector triggerSelector;
        [ShowInInspector]public UnityEvent triggerEvent
        {
            get
            {
                return triggerSelector?.item?.value as UnityEvent;
            }
            set { }
        }
        [HideInInspector, SerializeReference] public UnityAction action;
        [HideInInspector, OdinSerialize] public List<ItemSelector> stateSelectors;
        [ListDrawerSettings(ListElementLabelName = "stateName"),ShowInInspector] public List<SMSState> considerStates
        {
            get
            {
                List<SMSState> r = new List<SMSState>();
                if (stateSelectors != null && stateSelectors.Count != 0)
                {
                    stateSelectors.ForEach(x =>
                    {
                        if (x?.item?.value is SMSState state) r.Add(state);
                        else stateSelectors.Remove(x);
                    });
                }
                return r;
            }
            
        }
        
        public void SwitchStateFunction()
        {
            Debug.Log("called");
            if (!executer.entered) return;
            List<SMSState> considerList = new List<SMSState>(considerStates);
            considerList.RemoveAll(state => state.AbleToTransisst() == false);
            if (considerList.Count == 0) return;
            SMSState nextState = considerList[0];
            float currentDifference = nextState.GetDifference();
            foreach (SMSState thisConsider in considerList)
            {
                float thisiDifference = thisConsider.GetDifference();
                if (thisiDifference < currentDifference)
                {
                    nextState = thisConsider;
                    currentDifference = thisiDifference;
                }
            }
            executer.TransistToState(nextState);
        }
        public StateSwitchAction(DiscreteStateMachineExecuter executer, ItemSelector triggerSelector)
        {
            this.executer = executer;
            this.triggerSelector = new ItemSelector(triggerSelector);
            this.stateSelectors = new List<ItemSelector>();
            action = new UnityAction(SwitchStateFunction);
        }
        public void Enable()
        {
            triggerEvent.AddListener(action);
            triggerEvent.Invoke();
        }
        public void Disable()
        {
            triggerEvent.RemoveListener(action);
        }
        [Button]
        void De()
        {
            triggerEvent.Invoke();
        }
    }
    
}

