using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Nino.NewStateMatching
{
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
            set { }
        }
        
        public void SwitchStateFunction()
        { 
            if (!executer.entered) return;

            List<SMSState> considerList = new List<SMSState>(considerStates);
            SMSState currentState = executer.currentState;
            if (currentState != null && !currentState.selfTransform && considerList.Contains(currentState)) considerList.Remove(currentState);
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
            this.triggerSelector = triggerSelector.GetClone();
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
        void InvokeEvent()
        {
            triggerEvent.Invoke();
        }
    }
    
}

