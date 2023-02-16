using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
using Sirenix.OdinInspector;
namespace StateMatching.Action
{
    public class AnimatorExtension : Extension<Animator>
    {
        public AnimatorExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {

            
        }


    }
}

