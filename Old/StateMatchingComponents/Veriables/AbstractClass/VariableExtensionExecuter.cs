using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
using Nino.StateMatching;
using Sirenix.OdinInspector;
using System;

namespace Nino.StateMatching.Variable
{
    public abstract class VariableExtensionExecuter<V> : VariableGroupExtensionExecuter<V> 
    {
        [FoldoutGroup("Create Veriable"), Button(ButtonStyle.Box), GUIColor(0.4f, 1, 0.4f)]
        public void CreateItem(string name, V value)
        {
            Item<V> tryGet = groupController.GetItem(name);
            if (tryGet) return;
            Item<V> newItem = CreateNewItem() as Item<V>;
            newItem.value = value;
            newItem.itemName = name;
            groupController.AddItem(name, newItem);
        }
        [FoldoutGroup("Create Veriable"),Button(ButtonStyle.Box),GUIColor(0.4f,1,0.4f)]
        public void CreateItem(string name, string groupName,V value )
        {
            Item<V> tryGet = groupController.GetItem(name);
            if (tryGet) return;
            Item<V> newItem = CreateNewItem() as Item<V>;
            newItem.value = value;
            newItem.itemName = name;
            if (string.IsNullOrEmpty(groupName)) groupController.AddItem(name, newItem);
            else groupController.AddItem(name, newItem, groupName);
        }
        public abstract VariableItem<V> CreateNewItem();
    }
}

