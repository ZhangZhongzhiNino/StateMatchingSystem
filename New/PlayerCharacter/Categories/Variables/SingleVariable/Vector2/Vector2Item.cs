using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2Item : VariableItem<Vector2>
    {
        protected override void InitializeInstance()
        {
            value = Vector2.zero;
        }
    }
}

