
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolDataController : VariableDataController<BoolItem, bool>
    {
        protected override string WriteDataType()
        {
            return "Bool Variable";
        }
    }
}

