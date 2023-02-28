namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class VariableValue<T> : ItemValue
    {
        public T value;

        protected override void AssignItem(ItemValue newValue)
        {
            VariableValue<T> _newValue = newValue as VariableValue<T>;
            value = _newValue.value;
        }
    }
}

