namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolItem : VariableItem<bool, BoolValue>
    {
        protected override ItemValue CreateNewValue()
        {
            return new BoolValue();
        }
    }
}

