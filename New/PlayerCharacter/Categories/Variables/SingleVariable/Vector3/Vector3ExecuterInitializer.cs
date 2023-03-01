using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3ExecuterInitializer : ExecuterInitializer
    {
        public Vector3ExecuterInitializer(StateMatchingMonoBehaviour creater, string name) : base(creater, name)
        {
        }

        protected override StateMatchingMonoBehaviour AddComponentToGameObject(GameObject contentObj)
        {
            return contentObj.AddComponent<Vector3Executer>();
        }

        protected override StateMatchingMonoBehaviour TryFindContent()
        {
            return creater.GetComponentInChildren<Vector3Executer>();
        }
    }
}

