using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2DataController : VariableDataController<Vector2Item, Vector2>
    {
        protected override string WriteDataType()
        {
            return "Vector2 Variable";
        }
    }
}

