namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatExecuter : OldVariableExecuter<FloatItem, FloatDataController>
    {
        protected override string WriteLocalAddress()
        {
            return "Float";
        }
    }
}

