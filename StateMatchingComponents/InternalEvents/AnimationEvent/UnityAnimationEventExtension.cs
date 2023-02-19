using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.InternalEvent
{
    public class UnityAnimationEventExtension : Extension<UnityAnimationEventExtensionExecuter>
    {
        public UnityAnimationEventExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {
        }
    }
}

