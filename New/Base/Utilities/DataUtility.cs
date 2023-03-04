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
        public static Item AddItemToList(Item newItem, List<Item> list)
        {
            Item findItem = list.Find(x => x.itemName == newItem.itemName);
            if (findItem !=null) return findItem;
            list.Add(newItem);
            return newItem;
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
    }

}
