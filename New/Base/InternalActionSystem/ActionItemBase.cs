using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Nino.NewStateMatching
{
    [InlineEditor]
    public abstract class ActionItemBase<S> : DataScriptableObject 
        where S: SMSExecuter
    {
        public S script;
        public string actionName;
        public ActionInputVariable inputs;
        protected override void InitializeBaseScriptableObject()
        {
            
        }

        [Button(ButtonSizes.Large), GUIColor(1f, 1, 0.4f)]
        public abstract void PerformAction();
    }
    [InlineEditor]
    public abstract class ActionInputVariable : DataScriptableObject
    {
        [ReadOnly, SerializeField, TextArea, FoldoutGroup("Action Description")]
        string actionDescription;
        protected override void InitializeBaseScriptableObject()
        {
            actionDescription = WriteDescription();
        }
        protected abstract string WriteDescription();
    }
    public interface IActionWithVariableInput<V> where V: ActionInputVariable
    {
        V value { get; set; }
    } 
    [InlineEditor]
    public class ActionContainer<S> : StateMatchingScriptableObject
        where S: SMSExecuter
    {
        public List<ActionItemBase<S>> actions;
        public Dictionary<string, ActionItemBase<S>> dic_actions;
        public Dictionary<string, ActionInputVariable> dic_Values;
        protected override void Initialize()
        {
            actions = new List<ActionItemBase<S>>();
            dic_actions = new Dictionary<string, ActionItemBase<S>>();
        }
        public void UpdateDictionary()
        {
            if(dic_actions == null)dic_actions = new Dictionary<string, ActionItemBase<S>>();
            foreach(ActionItemBase<S> action in actions)
            {
                dic_actions.TryAdd(action.actionName, action);
            }
            if (dic_Values == null) dic_Values = new Dictionary<string, ActionInputVariable>();
            foreach(ActionItemBase<S> action in actions)
            {
                dic_Values.TryAdd(action.actionName,action.inputs);
            }
        }
    }

}

