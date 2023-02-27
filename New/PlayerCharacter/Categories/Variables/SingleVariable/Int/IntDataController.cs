
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntDataController : VariableDataController<IntItem, int>
    {
        protected override string WriteDataType()
        {
            return "Int Variable";
        }
    }
}

