
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringDataController : VariableDataController<StringItem, string>
    {
        protected override string WriteDataType()
        {
            return "String Variable";
        }
    }
}

