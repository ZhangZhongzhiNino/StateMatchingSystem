using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2DataController : VariableDataController<Vector2,Vector2Value>
    {
        protected override Item CreateNewItem()
        {
            return new Vector2Item();
        }

        protected override string WriteDataType()
        {
            return "Vector2 Variable";
        }
    }
}

