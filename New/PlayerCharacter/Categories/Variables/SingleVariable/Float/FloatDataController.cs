
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatDataController : VariableDataController<FloatItem, float>
    {
        protected override string WriteDataType()
        {
            return "Float Variable";
        }
    }
}

