using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Trigger.PlayerInput
{
    public class PlayerInputExecuterGroupInitializer : ExecuterGroupInitializer
    {
        public PlayerInputExecuterGroupInitializer(StateMatchingMonoBehaviour creater, string name) : base(creater, name)
        {
        }

        protected override StateMatchingMonoBehaviour AddComponentToGameObject(GameObject contentObj)
        {
            return contentObj.AddComponent<PlayerInputExecuterGroup>();
        }

        protected override StateMatchingMonoBehaviour TryFindContent()
        {
            return creater.GetComponentInChildren<PlayerInputExecuterGroup>();
        }
    }
}
