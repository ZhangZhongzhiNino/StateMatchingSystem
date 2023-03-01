using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatExecuterInitializer : ExecuterInitializer
    {
        public FloatExecuterInitializer(StateMatchingMonoBehaviour creater, string name) : base(creater, name)
        {
        }

        protected override StateMatchingMonoBehaviour AddComponentToGameObject(GameObject contentObj)
        {
            return contentObj.AddComponent<FloatExecuter>();
        }

        protected override StateMatchingMonoBehaviour TryFindContent()
        {
            return creater.GetComponentInChildren<FloatExecuter>();
        }
    }
}

