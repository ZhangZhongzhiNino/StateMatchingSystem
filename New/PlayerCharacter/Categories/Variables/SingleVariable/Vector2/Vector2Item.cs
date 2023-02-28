using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2Item : VariableItem<Vector2, Vector2Value>
    {
        public override ItemValue CreateNewValue()
        {
            return new Vector2Value();
        }
    }
}

