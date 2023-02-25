using UnityEditor;
using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter
{
    public class SMSPlayerCharacterRoot : StateMatchingRoot
    {
        public InputCategory inputCategory;
        public InternalEventCategory internalEventCategory;
        public VariableCategory variableCategory;
        public DataCategory dataCategory;
        public ActionCategory actionCategory;
        public StateMachineCategory stateMachineCategory;
        protected override void InitializeExecuterCategorys()
        {
            inputCategory = CreateCategory<InputCategory>("Input____");
            internalEventCategory = CreateCategory<InternalEventCategory>("Internal Event____");
            variableCategory = CreateCategory<VariableCategory>("Variable____");
            dataCategory = CreateCategory<DataCategory>("Data____");
            actionCategory = CreateCategory<ActionCategory>("Action____");
            stateMachineCategory = CreateCategory<StateMachineCategory>("FSM____");
        }
        protected override void RemoveExecuterTypes()
        {
            inputCategory.Remove();
            internalEventCategory.Remove();
            variableCategory.Remove();
            dataCategory.Remove();
            actionCategory.Remove();
            stateMachineCategory.Remove();
        }



        
    }
}
