using Sirenix.OdinInspector;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public abstract class VariableDataController<T,V> : DataController
        where V: VariableValue<T>,new()
    {
        protected override void InitializeInstance()
        {
            
        }
        protected override string WriteHint()
        {
            return "";
        }
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, V value)
        {
            AddItem(newItemName);
            VariableItem<T,V> getItem = items.Find(x => x.itemName == newItemName) as VariableItem<T,V>;
            getItem.InitiateValue();
            getItem.value.AssignValue(value);
            getItem.group = group;
        }
    }
}

