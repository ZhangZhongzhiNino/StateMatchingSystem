using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using Sirenix.OdinInspector;

using Nino.NewStateMatching;
using Nino.NewStateMatching.PlayerCharacter.Variable;

namespace Nino.NewStateMatching.PlayerCharacter
{
    public class SMSPlayerCharacterInitializer : StateMatchingRootInitializer<SMSPlayerCharacterRoot>
    {

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



        
    }
    public class InputCategory : ExecuterCategory
    {
        protected override void InitializeExecuterGroupInitializers()
        {
        }

        protected override void RemoveExecuterGroups()
        {
        }

        protected override void RemoveExecuters()
        {
            
        }

        protected override string WriteAddress()
        {
            return "Input";
        }
    }
    public class InternalEventCategory : ExecuterCategory
    {
        protected override void InitializeExecuterGroupInitializers()
        {
        }

        protected override void RemoveExecuterGroups()
        {
        }

        protected override void RemoveExecuters()
        {
            
        }

        protected override string WriteAddress()
        {
            return "Internal Event";
        }
    }
    public class VariableCategory : ExecuterCategory
    {
        public SingleVariableExecuterGroupInitializer singleVariableExecuterGroup;
        protected override void InitializeExecuterGroupInitializers()
        {
            singleVariableExecuterGroup = GeneralUtility.InitializeInitializer<SingleVariableExecuterGroupInitializer, SingleVariableExecuterGroup>(this);
        }

        protected override void RemoveExecuterGroups()
        {
        }

        protected override void RemoveExecuters()
        {
            
        }

        protected override string WriteAddress()
        {
            return "Variable";
        }
    }
    public class DataCategory : ExecuterCategory
    {
        protected override void InitializeExecuterGroupInitializers()
        {
        }

        protected override void RemoveExecuterGroups()
        {
        }

        protected override void RemoveExecuters()
        {
            
        }

        protected override string WriteAddress()
        {
            return "Data";
        }
    }
    public class ActionCategory : ExecuterCategory
    {
        protected override void InitializeExecuterGroupInitializers()
        {
        }

        protected override void RemoveExecuterGroups()
        {
        }

        protected override void RemoveExecuters()
        {
            
        }

        protected override string WriteAddress()
        {
            return "Action";
        }
    }
    public class StateMachineCategory : ExecuterCategory
    {
        protected override void InitializeExecuterGroupInitializers()
        {
        }

        protected override void RemoveExecuterGroups()
        {
        }

        protected override void RemoveExecuters()
        {
            
        }

        protected override string WriteAddress()
        {
            return "State Machine";
        }
    }
}
