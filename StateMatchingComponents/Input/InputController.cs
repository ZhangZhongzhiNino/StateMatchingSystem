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
            inputSystemContainer = Helpers.InitiateExtension<InputSystemContainerExtension>("Input System Container", this.gameObject, root);
            inputPresets = Helpers.InitiateExtension<InputPresetExtension>("Input Presets", this.gameObject,root);

        }
    }
}

