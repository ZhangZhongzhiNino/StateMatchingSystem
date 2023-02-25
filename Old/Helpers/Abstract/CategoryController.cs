using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Nino.StateMatching.Helper
{
    public abstract class CategoryController : MonoBehaviour,IStateMatchingComponent
    {
        [Button(ButtonSizes.Large), GUIColor(0.4f, 1, 1), PropertyOrder(-99999999999)]
        void ResetHierarchy()
        {
            EditorUtility.ResetHierachy(root.gameObject,this.gameObject);
        }
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
            actionType = ActionUtility.CreateActionType(GetActionTypeName(), this.gameObject, root.rootReferences.actionRoot);
            InitiateExtensions();
        }
        public abstract void InitiateExtensions();
        public abstract string GetActionTypeName();

    }
}

