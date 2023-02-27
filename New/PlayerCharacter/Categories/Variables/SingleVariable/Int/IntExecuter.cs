namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntExecuter : VariableExecuter<IntItem,IntDataController>
    {
        protected override string WriteLocalAddress()
        {
            return "Int";
        }
    }
}

