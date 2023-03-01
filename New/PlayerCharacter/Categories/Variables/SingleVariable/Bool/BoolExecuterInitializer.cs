using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolExecuterInitializer : ExecuterInitializer
    {
        public BoolExecuterInitializer(StateMatchingMonoBehaviour creater, string name) : base(creater, name)
        {
        }

        protected override StateMatchingMonoBehaviour AddComponentToGameObject(GameObject contentObj)
        {
            return contentObj.AddComponent<BoolExecuter>();
        }

        protected override StateMatchingMonoBehaviour TryFindContent()
        {
            return creater.GetComponentInChildren<BoolExecuter>();
        }
    }
}

