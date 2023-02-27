using Sirenix.OdinInspector;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public abstract class VariableDataController<Item,T> : DataController<Item>
        where Item : NewStateMatching.Item,new()
    {
        protected override void InitializeInstance()
        {
            
        }
        protected override string WriteHint()
        {
            return "";
        }
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, T value)
        {
            AddItem(newItemName);
            VariableItem<T> getItem = items.Find(x => x.itemName == newItemName) as VariableItem<T>;
            getItem.value = value;
            getItem.group = group;
        }
    }
}

