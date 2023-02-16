using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using StateMatching;

namespace StateMatching.Resource
{
    public class ResourceController : MonoBehaviour, IStateMatchingComponent
    {
        #region References
        [FoldoutGroup("Reference")]
        [TitleGroup("Reference/Reference"), SerializeField] StateMatchingRoot stateMatchingRoot;

        

        #endregion
        #region Initialize & Destroy
        public void Initialize<T>(T instance, StateMatchingRoot _stateMatchingRoot = null) where T : MonoBehaviour
        {
            stateMatchingRoot = _stateMatchingRoot;
        }

        public void PreDestroy()
        {
            
        }
        #endregion
    }
}

