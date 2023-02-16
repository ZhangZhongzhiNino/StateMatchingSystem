using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
namespace StateMatching.Data
{
    public class HumanoidInfoDataExtension : Extension<HumanoidInfoData>
    {
        public HumanoidInfoDataExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {
        }
    }
}
