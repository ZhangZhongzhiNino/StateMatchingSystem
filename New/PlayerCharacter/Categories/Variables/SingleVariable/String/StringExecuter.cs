namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringExecuter : VariableExecuter<StringItem, StringDataController>
    {
        protected override string WriteLocalAddress()
        {
            return "String";
        }
    }
}

