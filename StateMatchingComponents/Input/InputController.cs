using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
namespace StateMatching.Input
{
    public class InputController : CategoryController, IExtensionController
    {
        public InputSystemContainerExtension inputSystemContainer;
        public InputPresetExtension inputPresets;
        public void InitiateExtensions()
        {
            Helpers.SetUpExtensions<InputSystemContainerExtension>(ref inputSystemContainer, "Input System Container", this.gameObject, root);
            Helpers.SetUpExtensions<InputPresetExtension>(ref inputPresets, "Input Presets", this.gameObject, root);
        
        }
    }
}

