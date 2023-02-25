using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
using Sirenix.OdinInspector;
namespace Nino.StateMatching.InternalEvent
{
    public class InternalEventCategory : CategoryController
    {

        [FoldoutGroup("Reference")] public UnityAnimationEventExtension unityAnimationEventExtension;

        public override string GetActionTypeName()
        {
            return "InternalEvent";
        }

        public override void InitiateExtensions()
        {
            GeneralUtility.SetUpExtensions<UnityAnimationEventExtension>(ref unityAnimationEventExtension, "Unity Animation Event", this.gameObject, root);

        }
    }
}

