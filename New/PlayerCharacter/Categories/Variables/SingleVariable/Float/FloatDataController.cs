
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatDataController : VariableDataController<float,FloatValue>
    {
        protected override Item CreateNewItem()
        {
            return new FloatItem();
        }

        protected override string WriteDataType()
        {
            return "Float Variable";
        }
    }
}

