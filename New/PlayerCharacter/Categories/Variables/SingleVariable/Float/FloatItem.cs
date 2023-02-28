namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatItem : VariableItem<float, FloatValue>
    {
        public override ItemValue CreateNewValue()
        {
            return new FloatValue();
        }
    }
}

