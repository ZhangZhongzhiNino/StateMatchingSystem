namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatExecuter : VariableExecuter<FloatItem, FloatCollection, FloatDataController>
    {
        protected override string WriteLocalAddress()
        {
            return "Float";
        }
    }
}

