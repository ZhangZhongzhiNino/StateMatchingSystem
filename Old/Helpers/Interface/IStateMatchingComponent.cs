using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
namespace Nino.StateMatching
{
    public interface IStateMatchingComponent
    {
        public void Initiate<T>(T instance = null,StateMatchingRoot stateMatchingRoot=null) where T : MonoBehaviour;
        public void PreDestroy();
    }
}

