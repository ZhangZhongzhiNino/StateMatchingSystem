using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Nino.NewStateMatching.PlayerCharacter
{
    public class SMSPlayerCharacterRoot : StateMatchingRoot
    {
        [FoldoutGroup("Categories")] public InputCategory inputCategory;
        [FoldoutGroup("Categories")] public InternalEventCategory internalEventCategory;
        [FoldoutGroup("Categories")] public VariableCategory variableCategory;
        [FoldoutGroup("Categories")] public DataCategory dataCategory;
        [FoldoutGroup("Categories")] public ActionCategory actionCategory;
        [FoldoutGroup("Categories")] public StateMachineCategory stateMachineCategory;
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
