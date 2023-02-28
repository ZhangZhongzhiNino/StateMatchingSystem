using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3Value : VariableValue<Vector3>
    {
        public Vector3Value()
        {
            value = Vector3.zero;
        }
    }
}

