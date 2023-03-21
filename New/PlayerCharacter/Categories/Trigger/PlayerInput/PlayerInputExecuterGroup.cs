using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.Trigger.PlayerInput
{
    public class PlayerInputExecuterGroup : ExecuterGroup
    {
        protected override void AddExecuterInitializers()
        {
            GeneralUtility.AddExecuterInitializer(ref initializers, new ExecuterInitializer(this, "Touch Gesture",typeof(InputExecuter_TouchGesture)));
        }

        protected override string WriteLocalAddress()
        {
            return "Player Input";
        }
    }
}

