using System;
using System.Collections.Generic;
using UnityEngine;


namespace Nino.NewStateMatching
{
    public static class DataUtility
    {
        public static bool ScriptableObjectIsDefaultOrNull<T>(T obj) where T : class
        {
            return (obj == null || obj == default(T));
        }
        public static bool ListIsNullOrEmpty<T>(List<T> list)
        {
            return (list == null || list.Count == 0);
        }
        public static bool ListContainItem<T>(Predicate<T> match, List<T> list) where T : class
        {
            T getItem = GetItemInList<T>(match, list);
            return getItem != null;
        }
        public static T GetItemInList<T>(Predicate<T> match, List<T> list) where T : class
        {
            if (ListIsNullOrEmpty<T>(list)) return null;
            T getItem = list.Find(match);
            if (ScriptableObjectIsDefaultOrNull<T>(getItem)) return null;
            return getItem;
        }
        public static List<T> GetItemsInList<T>(Predicate<T> match, List<T> list) where T : class
        {
            if (DataUtility.ListIsNullOrEmpty<T>(list)) return null;
            List<T> r = list.FindAll(match);
            if (DataUtility.ListIsNullOrEmpty<T>(r)) return null;
            return r;
        }
        public static bool AddItemToList<T>(T newItem, List<T> list) where T : Item
        {
            if (ListContainItem<T>(item => item.itemName == newItem.itemName, list)) return false;
            list.Add(newItem);
            return true;
        }
        public static T AddItemToList<T>(string newItemName, List<T> list) where T : Item
        {
            if (ListContainItem(item => item.itemName == newItemName, list)) return GetItemInList(item => item.itemName == newItemName, list);
            T newItem = (T)Activator.CreateInstance<T>();
            newItem.Initialize();
            newItem.itemName = newItemName;
            if (AddItemToList(newItem, list)) return newItem;
            else throw new Exception("Unknow error in create new Item");
        }
        public static int RemoveItemsInList<T>(Predicate<T> match, List<T> list) where T : class
        {
            int r = list.FindAll(match).Count;
            list.RemoveAll(match);
            return r;
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
        public static T CopyScriptableObject<T>(T SO) where T : ScriptableObject
        {
            T r = ScriptableObject.CreateInstance<T>();
            string json = JsonUtility.ToJson(SO);
            JsonUtility.FromJsonOverwrite(json, r);
            return r;
        }
    }

}
