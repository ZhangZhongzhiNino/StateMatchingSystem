namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntExecuter : VariableExecuter<IntItem, IntCollection, IntDataController>
    {
        protected override string WriteLocalAddress()
        {
            return "Int";
        }
    }
}

