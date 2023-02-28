namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatItem : VariableItem<float, FloatValue>
    {
        protected override ItemValue CreateNewValue()
        {
            return new FloatValue();
        }
    }
}

