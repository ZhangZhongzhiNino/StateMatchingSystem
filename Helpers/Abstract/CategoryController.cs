using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Nino.StateMatching.Helper
{
    public abstract class CategoryController : MonoBehaviour,IStateMatchingComponent
    {
        [FoldoutGroup("Reference")] public StateMatchingRoot root;
        [FoldoutGroup("Reference")] public ActionType actionType;
        public virtual void PreDestroy(){ }
        public virtual void Initiate<T>(T instance = default, StateMatchingRoot stateMatchingRoot = null) where T : MonoBehaviour
        {
            if (stateMatchingRoot) root = stateMatchingRoot;
            if (instance)
            {
                CategoryController _instance = instance as CategoryController;
                root = _instance.root;
            }
            actionType = ActionUtility.CreateActionType(GetActionTypeName(), this.gameObject, root.actionRoot);
        }
        public abstract void InitiateExtensions();
        public abstract string GetActionTypeName();

    }
}

