using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Nino.StateMatching;
using Nino.StateMatching.Helper;
namespace Nino.StateMatching.Action
{
    public class ActionCategory : CategoryController
    {
        [FoldoutGroup("Reference")] public AnimatorExtension animatorExtension;
        [FoldoutGroup("Reference")] public CharacterControllerExtension characterControllerExtension;

        public override string GetActionTypeName()
        {
            return "Action";
        }

        public override void InitiateExtensions()
        {
            GeneralUtility.SetUpExtensions<AnimatorExtension>(ref animatorExtension, "animator", this.gameObject, root);
            GeneralUtility.SetUpExtensions<CharacterControllerExtension>(ref characterControllerExtension, "Character Controllers", this.gameObject, root);
        }
    }
    

}

