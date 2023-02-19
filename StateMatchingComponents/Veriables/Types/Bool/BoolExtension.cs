using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class BoolExtension : VariableExtension<BoolExtensionExecuter>
    {
        public BoolExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {

        }
    }
}

