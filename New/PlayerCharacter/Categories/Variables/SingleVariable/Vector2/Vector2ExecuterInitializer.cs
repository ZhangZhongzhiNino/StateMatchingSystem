using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2ExecuterInitializer : ExecuterInitializer
    {
        public Vector2ExecuterInitializer(StateMatchingMonoBehaviour creater, string name) : base(creater, name)
        {
        }

        protected override StateMatchingMonoBehaviour AddComponentToGameObject(GameObject contentObj)
        {
            return contentObj.AddComponent<Vector2Executer>();
        }

        protected override StateMatchingMonoBehaviour TryFindContent()
        {
            return creater.GetComponentInChildren<Vector2Executer>();
        }
    }
}

