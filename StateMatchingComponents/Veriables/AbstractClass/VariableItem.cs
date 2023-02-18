using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.Variable
{
    public class VariableItem<V> : Item<V>
    {

        public V _value;
        public override V value { get { return _value; } set { _value = value; } }

        public VariableItem()
        {
            itemName = "";
            value = default(V);

        }
    }
}



