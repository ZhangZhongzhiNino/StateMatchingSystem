using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
namespace StateMatching
{
    public interface IStateMatchingComponent
    {
        public void Initialize<T>(T instance = null,StateMatchingRoot stateMatchingRoot=null) where T : MonoBehaviour;
        public void PreDestroy();
    }
}

