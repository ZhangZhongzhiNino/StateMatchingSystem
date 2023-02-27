namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatExecuter : VariableExecuter<FloatItem,FloatDataController>
    {
        protected override string WriteLocalAddress()
        {
            return "Float";
        }
    }
}

