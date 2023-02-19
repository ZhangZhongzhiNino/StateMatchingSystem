using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Variable
{
    public abstract class VariableItem<V> : Item<V>
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



