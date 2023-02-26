using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2Item : VariableItem<Vector2>
    {
        public Vector2Item():base()
        {
            value = Vector2.zero;
        }
    }
}

