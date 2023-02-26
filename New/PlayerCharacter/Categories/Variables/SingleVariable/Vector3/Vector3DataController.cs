using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3DataController : VariableDataController<Vector3Item>
    {
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, Vector3 value)
        {
            AddItem(newItemName);
            Vector3Item getItem = GetItem(x => x.itemName == newItemName) as Vector3Item;
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "Vector3 Variable";
        }
        public override List<Item> GetItemsFromInstance()
        {
            return null;
        }
    }
}

