using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class SingleVariableExecuterInitializer : ExecuterInitializer
    {
        public SingleVariableExecuterInitializer(StateMatchingMonoBehaviour creater, string name) : base(creater, name)
        {
        }

        protected override StateMatchingMonoBehaviour AddComponentToGameObject(GameObject contentObj)
        {
            return contentObj.AddComponent<SingleVariableExecuter>();
        }
    }

}
