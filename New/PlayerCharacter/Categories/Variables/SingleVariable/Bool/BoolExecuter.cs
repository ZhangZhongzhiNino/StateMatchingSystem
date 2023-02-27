namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolExecuter : VariableExecuter<BoolItem,BoolDataController>
    {
        protected override string WriteLocalAddress()
        {
            return "Bool";
        }
    }
}

