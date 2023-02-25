using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Input
{
    public class InputSystemContainerExtension : Extension<InputSystemContainerExtensionExecuter>
    {
        public InputSystemContainerExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {
        }
    }
}

