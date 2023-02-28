
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringDataController : VariableDataController<string,StringValue>
    {
        protected override Item CreateNewItem()
        {
            return new StringItem();
        }

        protected override string WriteDataType()
        {
            return "String Variable";
        }
    }
}

