using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
using Sirenix.OdinInspector;
namespace Nino.StateMatching.Action
{
    public class AnimatorExtension : Extension<AnimatorExtensionExecuter>
    {
        public AnimatorExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {

            
        }


    }
}

