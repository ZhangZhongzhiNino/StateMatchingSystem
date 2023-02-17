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
            humanoidInfoDatas = Helpers.InitiateExtension<HumanoidInfoDataExtension>("Humanoid Info Data", this.gameObject, root);
            poseDatas = Helpers.InitiateExtension<PoseDataExtension>("Pose Data", this.gameObject, root);
        }
    }


}

