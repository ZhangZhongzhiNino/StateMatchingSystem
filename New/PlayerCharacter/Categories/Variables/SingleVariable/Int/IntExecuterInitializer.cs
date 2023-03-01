using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntExecuterInitializer : ExecuterInitializer
    {
        public IntExecuterInitializer(StateMatchingMonoBehaviour creater, string name) : base(creater, name)
        {
        }

        protected override StateMatchingMonoBehaviour AddComponentToGameObject(GameObject contentObj)
        {
            return contentObj.AddComponent<IntExecuter>();
        }

        protected override StateMatchingMonoBehaviour TryFindContent()
        {
            return creater.GetComponentInChildren<IntExecuter>();
        }
    }
}

