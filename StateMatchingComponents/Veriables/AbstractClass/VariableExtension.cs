using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.Variable
{
    public class VariableExtension<T> : Extension<T> where T : MonoBehaviour, IStateMatchingComponent
    {
        public VariableExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {
        }
    }
}

