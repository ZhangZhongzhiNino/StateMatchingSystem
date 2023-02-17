using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.Data
{
    public class PoseDataExtension : Extension<PoseDataManager>
    {
        public PoseDataExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {
        }
    }
}


