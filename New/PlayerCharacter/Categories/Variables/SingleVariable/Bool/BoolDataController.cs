
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolDataController : VariableDataController<BoolItem, BoolCollection>
    {
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, bool value)
        {
            collection.AddItem(newItemName);
            BoolItem getItem = collection.GetItem(x => x.itemName == newItemName);
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "Bool Variable";
        }
    }
}

