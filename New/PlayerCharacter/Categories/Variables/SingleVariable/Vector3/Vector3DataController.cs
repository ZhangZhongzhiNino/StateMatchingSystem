using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3DataController : VariableDataController<Vector3Item, Vector3Collection>
    {
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, Vector3 value)
        {
            collection.AddItem(newItemName);
            Vector3Item getItem = collection.GetItem(x => x.itemName == newItemName);
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "Vector3 Variable";
        }
    }
}

