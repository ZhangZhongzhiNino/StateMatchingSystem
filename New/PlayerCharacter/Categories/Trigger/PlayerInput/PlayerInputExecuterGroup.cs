using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Trigger.PlayerInput
{
    public class PlayerInputExecuterGroup : ExecuterGroup
    {
        protected override void AddExecuterInitializers()
        {
            GeneralUtility.AddExecuterInitializer(ref initializers, new InputExecuter_TouchGesture_Initializer(this, "Touch Gesture"));
        }

        protected override void RemoveExecuters()
        {

        }

        protected override string WriteLocalAddress()
        {
            return "Player Input";
        }
    }
}

