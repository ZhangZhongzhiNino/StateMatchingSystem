using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMatching.Variable
{
    public class BoolExtension : VariableExtension<BoolExecuter>
    {
        public BoolExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {

        }
    }
}

