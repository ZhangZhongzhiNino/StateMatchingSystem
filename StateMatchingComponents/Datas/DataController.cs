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
        [ShowInInspector] public HumanoidInfoDataExtension HumanoidInfoDatas;
        public void InitiateExtensions()
        {
            HumanoidInfoDatas = Helpers.InitiateExtension<HumanoidInfoDataExtension>("HumanoidInfoData", this.gameObject, root);
            
        }
    }


}

