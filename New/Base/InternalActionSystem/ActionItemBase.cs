using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Nino.NewStateMatching
{
    public class newActionInput : ActionInput
    {
        public int i;
    }
    [InlineEditor]
    public abstract class ActionInput
    {
        public bool b;
    }
    [InlineEditor]
    public abstract class ActionItemBase<S> 
        where S: SMSExecuter
    {
        [ReadOnly] public S script;
        [ReadOnly] public string actionName;
        public ActionInput input;
        [Button]
        void setinput()
        {
            input = new newActionInput();
        }
        public ActionItemBase(S script)
        {
            actionName = "";
            this.script = script;
        }

        [Button(ButtonSizes.Large), GUIColor(1f, 1, 0.4f)]
        public abstract void PerformAction();
        public abstract void AssignVariable(ActionItemBase<S> instance);
    }
    
    public abstract class ActionContainer<S>
        where S: SMSExecuter
    {
        [ReadOnly]public S script;
        public List<ActionItemBase<S>> actions;

        public ActionContainer(S script)
        {
            this.script = script;
            actions = new List<ActionItemBase<S>>();
        }
        public List<string> GetAllActionNames()
        {
            if (actions == null || actions.Count == 0) return null;
            List<string> r = new List<string>();
            foreach (ActionItemBase<S> action in actions)
            {
                r.Add(action.actionName);
            }
            return r;
        }
    }

}

