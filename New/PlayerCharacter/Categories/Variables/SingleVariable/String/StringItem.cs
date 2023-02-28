namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringItem : VariableItem<string, StringValue>
    {
        protected override ItemValue CreateNewValue()
        {
            return new StringValue();
        }
    }
}

