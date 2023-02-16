namespace StateMatching.Variable
{
    public class IntItem : VariableItem<IntItem, int>
    {
        public IntItem(string itemName, int itemValue)
        {
            this.itemName = itemName;
            this.value = itemValue;
        }
        public IntItem()
        {
            this.itemName = default(string);
            this.value = default(int);
        }
    }
}

