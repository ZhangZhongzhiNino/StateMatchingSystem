namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringExecuter : VariableExecuter<StringItem, StringCollection, StringDataController>
    {
        protected override string WriteLocalAddress()
        {
            return "String";
        }
    }
}

