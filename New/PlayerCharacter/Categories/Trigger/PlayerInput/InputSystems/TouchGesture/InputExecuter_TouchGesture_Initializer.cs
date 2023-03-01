using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Trigger.PlayerInput
{
    public class InputExecuter_TouchGesture_Initializer : ExecuterInitializer
    {
        public InputExecuter_TouchGesture_Initializer(StateMatchingMonoBehaviour creater, string name) : base(creater, name)
        {
            
        }

        protected override StateMatchingMonoBehaviour AddComponentToGameObject(GameObject contentObj)
        {
            return contentObj.AddComponent<InputExecuter_TouchGesture>();
        }

        protected override StateMatchingMonoBehaviour TryFindContent()
        {
            return creater.GetComponentInChildren<InputExecuter_TouchGesture>();
        }
    }
}

