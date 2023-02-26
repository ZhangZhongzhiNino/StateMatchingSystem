namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolExecuter : OldVariableExecuter<BoolItem, BoolDataController>
    {
        protected override string WriteLocalAddress()
        {
            return "Bool";
        }
    }
}

