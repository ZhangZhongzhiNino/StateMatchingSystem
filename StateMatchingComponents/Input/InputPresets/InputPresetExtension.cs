using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.Input 
{
    public class InputPresetExtension : Extension<InputPresets>
    {
        public InputPresetExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {
        }
    }
}


