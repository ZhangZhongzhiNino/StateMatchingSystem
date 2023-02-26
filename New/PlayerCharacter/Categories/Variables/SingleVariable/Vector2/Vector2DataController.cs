using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2DataController : VariableDataController<Vector2Item>
    {
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, Vector2 value)
        {
            AddItem(newItemName);
            Vector2Item getItem = GetItem(x => x.itemName == newItemName) as Vector2Item;
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "Vector2 Variable";
        }
        public override List<Item> GetItemsFromInstance()
        {
            return null;
        }
    }
}

