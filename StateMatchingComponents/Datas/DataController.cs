using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using StateMatching;
using StateMatching.Helper;

namespace StateMatching.Data
{
    public class DataController : CategoryController, IExtensionController
    {
        public HumanoidInfoDataExtension humanoidInfoDatas;
        public PoseDataExtension poseDatas;
        public void InitiateExtensions()
        {
            Helpers.SetUpExtensions<HumanoidInfoDataExtension>(ref humanoidInfoDatas, "Humanoid Info Data", this.gameObject, root);
            Helpers.SetUpExtensions<PoseDataExtension>(ref poseDatas, "Pose Data", this.gameObject, root);
        }
    }


}

