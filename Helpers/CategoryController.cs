using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace StateMatching.Helper
{
    public class CategoryController : MonoBehaviour,IStateMatchingComponent
    {
        public StateMatchingRoot root;
        public void PreDestroy()
        {
            
        }

        public void Initialize<T>(T instance = default, StateMatchingRoot stateMatchingRoot = null) where T : MonoBehaviour
        {
            if (stateMatchingRoot) root = stateMatchingRoot;
            if (!instance) return;
            CategoryController _instance = instance as CategoryController;
            root = _instance.root;
        }
    }
}

