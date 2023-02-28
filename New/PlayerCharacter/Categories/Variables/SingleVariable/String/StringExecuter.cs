using Sirenix.OdinInspector;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringExecuter : VariableExecuter<string,StringValue, StringDataController>
    {

        protected override void InitilizeActionContainer()
        {

        }
        protected override string WriteLocalAddress()
        {
            return "String";
        }
    }
}

