using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3Item : VariableItem<Vector3>
    {
        public Vector3Item(): base()
        {
            value = Vector3.zero;
        }
    }
}

