
using Sirenix.OdinInspector;
using System.Collections.Generic;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringDataController : VariableDataController<StringItem>
    {
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, string value)
        {
            AddItem(newItemName);
            StringItem getItem = GetItem(x => x.itemName == newItemName) as StringItem;
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "String Variable";
        }
        public override List<Item> GetItemsFromInstance()
        {
            return null;
        }
    }
}

