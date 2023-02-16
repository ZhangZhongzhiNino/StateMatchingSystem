namespace StateMatching.Variable
{
    public class FloatItem : VariableItem<FloatItem, float>
    {
        public FloatItem(string itemName, float itemValue)
        {
            this.itemName = itemName;
            this.value = itemValue;
        }
        public FloatItem()
        {
            this.itemName = default(string);
            this.value = default(float);
        }
    }
}

