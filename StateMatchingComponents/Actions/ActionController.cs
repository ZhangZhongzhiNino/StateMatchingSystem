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
        public AnimatorExtension animator;
        public CharacterControllerExtension characterControllers;
        public void InitiateExtensions()
        {
            Helpers.SetUpExtensions<AnimatorExtension>(ref animator, "animator", this.gameObject, root);
            Helpers.SetUpExtensions<CharacterControllerExtension>(ref characterControllers, "Character Controllers", this.gameObject, root);
        }
    }
    

}

