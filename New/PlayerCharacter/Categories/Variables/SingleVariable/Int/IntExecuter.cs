using Sirenix.OdinInspector;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntExecuter : VariableExecuter<int,IntValue,IntDataController>
    {
        protected override void InitilizeActionContainer()
        {

        }

        protected override string WriteLocalAddress()
        {
            return "Int";
        }
    }
}

