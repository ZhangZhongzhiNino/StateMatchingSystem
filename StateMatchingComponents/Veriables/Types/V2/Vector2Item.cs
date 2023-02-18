using UnityEngine;

namespace StateMatching.Variable
{
    public class Vector2Item : VariableItem<Vector2>
    {
        public Vector2Item(string itemName, Vector2 itemValue)
        {
            this.itemName = itemName;
            this.value = itemValue;
        }
        public Vector2Item()
        {
            this.itemName = default(string);
            this.value = default(Vector2);
        }
    }
}

