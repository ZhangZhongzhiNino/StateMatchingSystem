
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolDataController : VariableDataController<BoolItem>
    {
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, bool value)
        {
            AddItem(newItemName);
            BoolItem getItem = GetItem(x => x.itemName == newItemName) as BoolItem;
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "Bool Variable";
        }

        public override List<Item> GetItemsFromInstance()
        {
            return null;
        }
    }
}

