namespace Nino.StateMatching.Variable
{
    public class StringItem : VariableItem<string>
    {
        public StringItem(string itemName, string itemValue)
        {
            this.itemName = itemName;
            this.value = itemValue;
        }
        public StringItem()
        {
            this.itemName = default(string);
            this.value = default(string);
        }
    }
}

