using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMatching.Variable
{
    public class QuaternionItem : VariableItem<QuaternionItem, Quaternion>
    {
        public QuaternionItem(string itemName, Quaternion itemValue)
        {
            this.itemName = itemName;
            this.value = itemValue;


        }
        public QuaternionItem()
        {
            this.itemName = default(string);
            this.value = default(Quaternion);
        }

    }
}

