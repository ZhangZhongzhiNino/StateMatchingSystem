using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
using StateMatching;
using Sirenix.OdinInspector;
using System;

namespace StateMatching.Variable
{
    public abstract class VariableExecuter<T,V> : StructGroupExtensionExecuter<T,V> where T:MonoBehaviour,IGroupItem<T,V> where V: struct
    {
        [FoldoutGroup("Create Veriable"), Button(ButtonStyle.Box), GUIColor(0.4f, 1, 0.4f)]
        public void CreateItem(string name, V value)
        {
            T tryGet = groupController.GetItem(name);
            if (tryGet) return;
            T newItem = CreateNewItem() as T;
            newItem.value = value;
            newItem.itemName = name;
            groupController.AddItem(name, newItem);
        }
        [FoldoutGroup("Create Veriable"),Button(ButtonStyle.Box),GUIColor(0.4f,1,0.4f)]
        public void CreateItem(string name, string groupName,V value )
        {
            T tryGet = groupController.GetItem(name);
            if (tryGet) return;
            T newItem = CreateNewItem() as T;
            newItem.value = value;
            newItem.itemName = name;
            if (string.IsNullOrEmpty(groupName)) groupController.AddItem(name, newItem);
            else groupController.AddItem(name, newItem, groupName);
        }
        public abstract MonoBehaviour CreateNewItem();
    }
}

