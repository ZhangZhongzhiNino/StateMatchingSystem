using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3DataController : VariableDataController<Vector3Item,Vector3>
    {
        
        protected override string WriteDataType()
        {
            return "Vector3 Variable";
        }
    }
}

