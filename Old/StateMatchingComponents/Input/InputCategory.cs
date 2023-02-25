using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
using Sirenix.OdinInspector;
namespace Nino.StateMatching.Input
{
    public class InputCategory : CategoryController
    {
        [FoldoutGroup("Reference")] public InputSystemContainerExtension inputSystemContainerExtension;
        [FoldoutGroup("Reference")] public InputPresetExtension inputPresetsExtension;

        public override string GetActionTypeName()
        {
            return "Input";
        }

        public override void InitiateExtensions()
        {
            GeneralUtility.SetUpExtensions<InputSystemContainerExtension>(ref inputSystemContainerExtension, "Input System Container", this.gameObject, root);
            GeneralUtility.SetUpExtensions<InputPresetExtension>(ref inputPresetsExtension, "Input Presets", this.gameObject, root);
        
        }
    }
}

