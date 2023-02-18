using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace StateMatching.Helper
{
    public abstract class ExtensionExecuter : MonoBehaviour,IStateMatchingComponent 
    {
        [PropertyOrder(-9999999), FoldoutGroup("Reference")] public StateMatchingRoot root;
        public abstract CategoryController getCategory();
        public virtual void Initialize<_T>(_T instance = null, StateMatchingRoot stateMatchingRoot = null) where _T : MonoBehaviour
        {
            root = stateMatchingRoot;
            
        }
        public virtual void PreDestroy()
        {
            Helpers.RemoveGameObject(this.gameObject);
        }
        [Button(ButtonSizes.Large), GUIColor(0.4f, 1, 1),PropertyOrder(-99999999999)]
        void ResetHierarchy()
        {
            Helpers.ResetHierachy(root.gameObject, getCategory().gameObject);
        }
    }
}

