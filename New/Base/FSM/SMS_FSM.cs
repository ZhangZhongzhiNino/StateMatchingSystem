using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Nino.NewStateMatching
{
    public class StateAction
    {
        public int actionId; // if more than 1 same action method in state difference them with this id
        public ActionMethod actionMethodReference;
        public ActionInput input;
        public StateAction(ref List<StateAction> list, ActionMethod method)
        {
            actionId = list.FindAll(x => x.actionMethodReference.actionName == method.actionName).Count;
            actionMethodReference = method;
            input = method.CreateActionInput();
        }
    }
    public class SMS_State
    {
        public string stateName;
        public List<StateAction> actions;
        public SMS_State(string stateName)
        {
            this.stateName = stateName;
            actions = new List<StateAction>();
        }
    }
    public class SMS_FSM : SMSExecuter
    {
        [HideInInspector] public List<SMS_State> stateCache;
        [ListDrawerSettings(ListElementLabelName = "stateName",HideAddButton = true)] public List<SMS_State> states;
        protected override void InitializeInstance()
        {
            if (states == null) states = new List<SMS_State>();
            if (stateCache == null) stateCache = new List<SMS_State>();
        }

        protected override void PreRemoveInstance()
        {
            
        }

        [Button(Style = ButtonStyle.Box, ButtonHeight = 30), GUIColor(0.4f, 1, 0.4f)]
        void CreateNewState(string newStateName = "New State")
        {
            if (states == null) states = new List<SMS_State>();
            if (stateCache == null) stateCache = new List<SMS_State>();
            if(states.Find(x=>x.stateName == newStateName) != null)
            {
                int addToName = 1;
                while (states.Find(x => x.stateName == newStateName + addToName) != null) addToName++;
                newStateName = newStateName + addToName;
            }
            SMS_State newState = new SMS_State(newStateName);
            states.Add(newState);
            stateCache.Add(newState);
        }
        [Button]
        void RevertRemove()
        {
            states = new List<SMS_State>(stateCache);
        }
        [Button]
        void UpdateCache()
        {
            stateCache = new List<SMS_State>( states);
        }
    }
    public class FSM_Group : ExecuterGroup
    {
        protected override void AddExecuterInitializers()
        {
            
        }

        protected override string WriteLocalAddress()
        {
            return "State Machines";
        }
        [Button(Style = ButtonStyle.Box,ButtonHeight = 30),GUIColor(0.4f,1,0.4f)]
        public void AddFSM(string newFSMname = "New State Machine")
        {
            if (FSMs.Find(x => x.address.localAddress == newFSMname) != default(SMS_FSM))
            {
                int addToName = 1;
                while (FSMs.Find(x => x.address.localAddress == newFSMname + addToName) )addToName++;
                newFSMname = newFSMname + addToName;
            }
            
            GameObject newObj = GeneralUtility.CreateGameObject("<" + newFSMname + ">", this.transform);
            SMS_FSM newFSM = newObj.AddComponent<SMS_FSM>();
            newFSM.Initialize();
            newFSM.address.localAddress = newFSMname;
            newFSM.address.UpdateGlobalAddressInChild();
            FSMs.Add(newFSM);
            ResetHierarchy();
        }
        [Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)]
        public void RemoveNullInFSMs()
        {
            FSMs.RemoveAll(x => x == null);
        }
    }
    public class FSM_Group_Initializer : ExecuterGroupInitializer
    {
        public FSM_Group_Initializer(StateMatchingMonoBehaviour creater, string name) : base(creater, name)
        {
        }

        protected override StateMatchingMonoBehaviour AddComponentToGameObject(GameObject contentObj)
        {
            return contentObj.AddComponent<FSM_Group>();
        }

        protected override StateMatchingMonoBehaviour TryFindContent()
        {
            return creater.GetComponentInChildren<FSM_Group>();
        }
    }

}

