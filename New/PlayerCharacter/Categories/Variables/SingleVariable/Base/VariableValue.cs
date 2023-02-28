namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class VariableValue<T> : ItemValue
    {
        public T value;

        public override void AssignValue(ItemValue newValue)
        {
            VariableValue<T> _newValue = newValue as VariableValue<T>;
            value = _newValue.value;
        }
    }
}

