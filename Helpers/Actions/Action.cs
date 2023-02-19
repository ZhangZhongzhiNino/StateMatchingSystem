using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
namespace Nino.StateMatching.Helper
{
    public abstract class Action : MonoBehaviour
    {
        [ReadOnly] public string actionName;
        [ReadOnly] public ExtensionExecuter executerReference;
        public abstract void BeforeAction();
        public abstract void PerformAction();
        public abstract void AfterAction();

        public virtual void Initiate( string actionName, ExtensionExecuter executerReference)
        {
            this.actionName = actionName;
            this.executerReference = executerReference;
        }
        
    }
    public class ActionGroup : MonoBehaviour
    {
        [ReadOnly] public string groupName;
        [ReadOnly] public List<Action> actions;
        public void Initiate(string groupName)
        {
            this.groupName = groupName;
            actions = new List<Action>();
        }
        private void OnDrawGizmos()
        {
            actions.RemoveAll(x => x == null);
        }
    }
    public class ActionType: MonoBehaviour
    {
        [ReadOnly] public string typeName;
        [ReadOnly] public List<ActionGroup> groups;
        public void Initiate(string categoryName)
        {
            this.typeName = categoryName;
            groups = new List<ActionGroup>();
        }
        private void OnDrawGizmos()
        {
            groups.RemoveAll(x => x == null);
        }
    }
    public class ActionRoot : MonoBehaviour
    {
        public string rootName;
        [ReadOnly] public List<ActionType> types;
        public void Initiate(string rootName = "")
        {
            this.rootName = rootName;
            types = new List<ActionType>();
        }
        private void OnDrawGizmos()
        {
            types.RemoveAll(x => x == null);
        }
    }
}

