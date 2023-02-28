namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringItem : VariableItem<string, StringValue>
    {
        public override ItemValue CreateNewValue()
        {
            return new StringValue();
        }
    }
}

