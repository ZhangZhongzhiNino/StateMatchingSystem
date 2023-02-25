namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolExecuter : VariableExecuter<BoolItem, BoolCollection, BoolDataController>
    {
        protected override string WriteLocalAddress()
        {
            return "Bool";
        }
    }
}

