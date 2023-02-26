
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatDataController : VariableDataController<FloatItem>
    {
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, float value)
        {
            AddItem(newItemName);
            FloatItem getItem = GetItem(x => x.itemName == newItemName) as FloatItem;
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "Float Variable";
        }
        public override List<Item> GetItemsFromInstance()
        {
            return null;
        }
    }
}

