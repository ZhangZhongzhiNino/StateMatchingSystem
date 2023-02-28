using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3DataController : VariableDataController<Vector3,Vector3Value>
    {
        protected override Item CreateNewItem()
        {
            return new Vector3Item();
        }

        protected override string WriteDataType()
        {
            return "Vector3 Variable";
        }
    }
}

