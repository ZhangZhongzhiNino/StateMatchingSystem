using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Nino.StateMatching;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Data
{
    public class DataCategory : CategoryController
    {
        [FoldoutGroup("Reference")] public HumanoidInfoDataExtension humanoidInfoDataExtension;
        [FoldoutGroup("Reference")] public PoseDataExtension poseDataExtension;

        public override string GetActionTypeName()
        {
            return "Data";
        }

        public override void InitiateExtensions()
        {
            GeneralUtility.SetUpExtensions<HumanoidInfoDataExtension>(ref humanoidInfoDataExtension, "Humanoid Info Data", this.gameObject, root);
            GeneralUtility.SetUpExtensions<PoseDataExtension>(ref poseDataExtension, "Pose Data", this.gameObject, root);
        }
    }


}

