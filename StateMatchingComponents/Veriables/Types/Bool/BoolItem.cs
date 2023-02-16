namespace StateMatching.Variable
{
    public class BoolItem : VariableItem<BoolItem, bool>
    {
        public BoolItem(string itemName, bool itemValue)
        {
            this.itemName = itemName;
            this.value = itemValue;
        }
        public BoolItem()
        {
            this.itemName = default(string);
            this.value = default(bool);
        }
    }

}

