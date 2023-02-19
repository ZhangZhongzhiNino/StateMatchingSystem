using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
namespace Nino.StateMatching.Data
{
    public class HumanoidInfoDataExtension : Extension<HumanoidInfoDataExtensionExecuter>
    {
        public HumanoidInfoDataExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {
        }
    }
}
