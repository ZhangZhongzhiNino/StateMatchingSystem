using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3Item : VariableItem<Vector3>
    {
        protected override void InitializeInstance()
        {
            value = Vector3.zero;
        }
    }
}

