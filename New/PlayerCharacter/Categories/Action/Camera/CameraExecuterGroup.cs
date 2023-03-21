using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nino.NewStateMatching.Action
{
    public class CameraExecuterGroup : ExecuterGroup
    {
        protected override void AddExecuterInitializers()
        {
            GeneralUtility.AddExecuterInitializer(ref initializers, new ExecuterInitializer(this, "Cinemachine", typeof(CinemachineExecuter)));
        }

        protected override string WriteLocalAddress()
        {
            return "Camera Control";
        }
    }
}

