using System;
using System.Collections.Generic;
using UnityEngine;


namespace Nino.NewStateMatching
{
    public static class DataUtility
    {
        public static bool ScriptableObjectIsDefaultOrNull<T>(T obj) where T : ScriptableObject
        {
            return (obj == null || obj == default(T));
        }
        public static bool ListIsNullOrEmpty<T>(List<T> list)
        {
            return (list == null || list.Count == 0);
        }
        public static bool ListContainItem<T>(Predicate<T> match, List<T> list) 
        {
            T getItem = list.Find(match);
            return getItem != null;
        }
        public static bool AddItemToList<T>(T newItem, List<T> list) where T : Item, new()
        {
            if (list.Contains(newItem)) return false;
            list.Add(newItem);
            return true;
        }
        public static T AddItemToList<T>(string newItemName, List<T> list) where T : Item,new()
        {
            if (ListContainItem(item => item.itemName == newItemName, list)) return list.Find(item => item.itemName == newItemName);
            T newItem = new T();
            newItem.itemName = newItemName;
            if (AddItemToList(newItem, list)) return newItem;
            else throw new Exception("Unknow error in create new Item");
        }
        public static List<string> RemoveAllRedundantStringInList(List<string> list)
        {
            list.RemoveAll(x => string.IsNullOrEmpty(x));
            list.RemoveAll(x => string.IsNullOrWhiteSpace(x));
            List<string> newList = new List<string>();
            foreach (string tag in list)
            {
                if (!newList.Contains(tag)) newList.Add(tag);
            }
            return newList;
        }
        /*public static T CopyScriptableObject<T>(T SO) where T : ScriptableObject
        {
            T r = ScriptableObject.CreateInstance<T>();
            string json = JsonUtility.ToJson(SO);
            JsonUtility.FromJsonOverwrite(json, r);
            return r;
        }*/
    }

}
