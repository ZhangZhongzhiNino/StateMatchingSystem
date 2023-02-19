using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Action
{
    public class CharacterControllerExtension : Extension<CharacterControllerExtensionExecuter>
    {
        public CharacterControllerExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {
        }
    }
}

