using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace StateMatching.Helper
{
    public class ExtensionExecuter : MonoBehaviour,IStateMatchingComponent 
    {
        [PropertyOrder(-9999999), FoldoutGroup("Reference")] public StateMatchingRoot root;
        public virtual void Initialize<_T>(_T instance = null, StateMatchingRoot stateMatchingRoot = null) where _T : MonoBehaviour
        {
            root = stateMatchingRoot;
            
        }
        public virtual void PreDestroy()
        {
            Helpers.RemoveGameObject(this.gameObject);
        }
    }
}

