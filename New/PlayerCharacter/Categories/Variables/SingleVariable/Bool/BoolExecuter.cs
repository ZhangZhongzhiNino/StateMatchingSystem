using Sirenix.OdinInspector;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolExecuter : VariableExecuter<bool,BoolValue,BoolDataController>
    {
        protected override void InitilizeActionContainer()
        {

        }
        protected override string WriteLocalAddress()
        {
            return "Bool";
        }
    }
}

