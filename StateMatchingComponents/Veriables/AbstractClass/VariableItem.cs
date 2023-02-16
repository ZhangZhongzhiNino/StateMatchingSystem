using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.Variable
{
    public class VariableItem<T, V> : MonoBehaviour, IGroupItem<T, V> where T : MonoBehaviour where V : struct
    {
        public string variableName;
        public V _value;
        public string itemName { get { return variableName; } set { variableName = value; } }
        public V value { get { return _value; } set { _value = value; } }

        public void AssignItem(T item)
        {
            VariableItem<T, V> _item = item as VariableItem<T, V>;
            variableName = _item.variableName;
            _value = _item._value;
        }
        public VariableItem()
        {
            variableName = "";
            _value = default(V);

        }
    }
}



