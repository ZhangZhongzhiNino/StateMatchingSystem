using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class SingleVariableExecuterGroupInitializer : ExecuterGroupInitializer
    {
        public SingleVariableExecuterGroupInitializer(StateMatchingMonoBehaviour creater, string name) : base(creater, name)
        {
        }

        protected override StateMatchingMonoBehaviour AddComponentToGameObject(GameObject contentObj)
        {
            return contentObj.AddComponent<SingleVariableExecuterGroup>();
        }

        protected override StateMatchingMonoBehaviour TryFindContent()
        {
            return creater.GetComponentInChildren<SingleVariableExecuterGroup>();
        }
    }
}