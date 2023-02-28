using Sirenix.OdinInspector;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatExecuter : VariableExecuter<float,FloatValue,FloatDataController>
    {

        protected override void InitilizeActionContainer()
        {

        }
        protected override string WriteLocalAddress()
        {
            return "Float";
        }
    }
}

