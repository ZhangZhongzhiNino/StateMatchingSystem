using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using Sirenix.OdinInspector;

using Nino.NewStateMatching;

namespace Nino.NewStateMatching.PlayerCharacter
{
    public class SMSPlayerCharacterInitializer : StateMatchingInitializer<SMSPlayerCharacterRoot>
    {
        protected override void ResetHierarchy()
        {
            
        }
    }
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
        protected override void ResetHierarchy()
        {
            
        }
        public override void Refresh()
        {
            inputCategory.Refresh();
            internalEventCategory.Refresh();
            variableCategory.Refresh();
            dataCategory.Refresh();
            actionCategory.Refresh();
            stateMachineCategory.Refresh();
        }



        
    }
    public class InputCategory : ExecuterCategory
    {
        public override void Refresh()
        {
        }
        protected override void InitializeExecuterGroupInitializers()
        {
        }
        protected override void RemoveExecuterGroups()
        {
        }
        protected override void ResetHierarchy()
        {
        }
    }
    public class InternalEventCategory : ExecuterCategory
    {
        public override void Refresh()
        {
        }
        protected override void InitializeExecuterGroupInitializers()
        {
        }
        protected override void RemoveExecuterGroups()
        {
        }
        protected override void ResetHierarchy()
        {
        }
    }
    public class VariableCategory : ExecuterCategory
    {
        public override void Refresh()
        {
        }
        protected override void InitializeExecuterGroupInitializers()
        {
        }
        protected override void RemoveExecuterGroups()
        {
        }
        protected override void ResetHierarchy()
        {
        }
    }
    public class DataCategory : ExecuterCategory
    {
        public override void Refresh()
        {
        }
        protected override void InitializeExecuterGroupInitializers()
        {
        }
        protected override void RemoveExecuterGroups()
        {
        }
        protected override void ResetHierarchy()
        {
        }
    }
    public class ActionCategory : ExecuterCategory
    {
        public override void Refresh()
        {
        }
        protected override void InitializeExecuterGroupInitializers()
        {
        }
        protected override void RemoveExecuterGroups()
        {
        }
        protected override void ResetHierarchy()
        {
        }
    }
    public class StateMachineCategory : ExecuterCategory
    {
        public override void Refresh()
        {
        }
        protected override void InitializeExecuterGroupInitializers()
        {
        }
        protected override void RemoveExecuterGroups()
        {
        }
        protected override void ResetHierarchy()
        {
        }
    }
}
