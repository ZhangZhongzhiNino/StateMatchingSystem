using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3Item : VariableItem<Vector3, Vector3Value>
    {
        protected override ItemValue CreateNewValue()
        {
            return new Vector3Value();
        }
    }
}

