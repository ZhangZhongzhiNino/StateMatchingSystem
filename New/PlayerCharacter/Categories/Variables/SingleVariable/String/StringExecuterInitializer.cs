using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringExecuterInitializer : ExecuterInitializer
    {
        public StringExecuterInitializer(StateMatchingMonoBehaviour creater, string name) : base(creater, name)
        {
        }

        protected override StateMatchingMonoBehaviour AddComponentToGameObject(GameObject contentObj)
        {
            return contentObj.AddComponent<StringExecuter>();
        }

        protected override StateMatchingMonoBehaviour TryFindContent()
        {
            return creater.GetComponentInChildren<StringExecuter>();
        }
    }
}

