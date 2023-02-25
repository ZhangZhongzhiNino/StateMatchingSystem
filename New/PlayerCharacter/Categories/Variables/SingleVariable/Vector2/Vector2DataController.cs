using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2DataController : VariableDataController<Vector2Item, Vector2Collection>
    {
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, Vector2 value)
        {
            collection.AddItem(newItemName);
            Vector2Item getItem = collection.GetItem(x => x.itemName == newItemName);
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "Vector2 Variable";
        }
    }
}

