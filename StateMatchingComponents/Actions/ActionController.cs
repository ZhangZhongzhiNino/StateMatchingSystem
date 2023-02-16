using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using StateMatching;
using StateMatching.Helper;
namespace StateMatching.Action
{
    public class ActionController : CategoryController, IExtensionController
    {
        [ShowInInspector]public AnimatorExtension animator;
        public void InitiateExtensions()
        {
            animator = Helpers.InitiateExtension<AnimatorExtension>("animator", this.gameObject, root);
        }
    }
    

}
