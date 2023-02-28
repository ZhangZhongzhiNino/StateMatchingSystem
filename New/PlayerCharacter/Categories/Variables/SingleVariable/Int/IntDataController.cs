
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntDataController : VariableDataController<int,IntValue>
    {
        protected override Item CreateNewItem()
        {
            return new IntItem();
        }

        protected override string WriteDataType()
        {
            return "Int Variable";
        }
    }
}

