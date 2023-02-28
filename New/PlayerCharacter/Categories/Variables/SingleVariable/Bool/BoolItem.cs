namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolItem : VariableItem<bool, BoolValue>
    {
        public override ItemValue CreateNewValue()
        {
            return new BoolValue();
        }
    }
}

