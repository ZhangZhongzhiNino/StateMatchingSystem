using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMatching.Helper
{
    public class ExtensionExecuter : MonoBehaviour,IStateMatchingComponent 
    {
        public StateMatchingRoot root;
        public virtual void Initialize<_T>(_T instance = null, StateMatchingRoot stateMatchingRoot = null) where _T : MonoBehaviour
        {
            root = stateMatchingRoot;
            
        }
        public void PreDestroy()
        {
            Helpers.RemoveGameObject(this.gameObject);
        }
    }
}

