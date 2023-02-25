namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public abstract class VariableItem<T> : Item
    {
        public T value;
        protected override void AssignItem(Item newItem)
        {
            VariableItem<T> _item = newItem as VariableItem<T>;
            value = _item.value;
        }
    }
}

