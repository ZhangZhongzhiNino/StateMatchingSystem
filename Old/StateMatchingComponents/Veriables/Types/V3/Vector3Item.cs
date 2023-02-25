using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class Vector3Item : VariableItem< Vector3>
    {
        public Vector3Item(string itemName, Vector3 itemValue)
        {
            this.itemName = itemName;
            this.value = itemValue;
        }
        public Vector3Item()
        {
            this.itemName = default(string);
            this.value = default(Vector3);
        }
    }
}

