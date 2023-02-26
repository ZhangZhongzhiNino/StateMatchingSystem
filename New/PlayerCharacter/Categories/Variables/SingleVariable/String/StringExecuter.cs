namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringExecuter : OldVariableExecuter<StringItem, StringDataController>
    {
        protected override string WriteLocalAddress()
        {
            return "String";
        }
    }
}

