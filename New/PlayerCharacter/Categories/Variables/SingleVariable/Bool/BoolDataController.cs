
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolDataController : VariableDataController<bool,BoolValue>
    {
        protected override Item CreateNewItem()
        {
            return new BoolItem();
        }

        protected override string WriteDataType()
        {
            return "Bool Variable";
        }
    }
}

