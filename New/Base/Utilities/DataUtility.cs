using System;
using System.Collections.Generic;
using UnityEngine;


namespace Nino.NewStateMatching
{
    public static class DataUtility
    {
        public static void AddAction(
            DataController dataController,
            string actionName,
            Action<object> actionFunction,
            System.Type inputType = null,
            bool needItemReference =false,
            bool continuous= false)
        {
            bool haveInput = inputType != null;

            SMSAction newAction = new SMSAction(
                        actionName,
                        actionFunction,
                        inputType,
                        haveInput,
                        needItemReference,
                        continuous); 
            dataController.AddItem(
                new LabledItem(
                    newAction,
                    actionName,
                    "Action"
                ));
        }
        public static void AddCompair(
            DataController dataController, 
            string compairName,
            CompairFucntion func, 
            System.Type inputType, 
            System.Type targetType)
        {
            CompairMethod newCompair = new CompairMethod(compairName,func, inputType, targetType);
            LabledItem newLabledItem = new LabledItem(newCompair, compairName, "Compair");
            dataController.AddItem(newLabledItem);
        }
        public static void AddTFCompair(
            DataController dataController,
            string compairName,
            TFCompairFucntion func,
            System.Type inputType,
            System.Type targetType)
        {
            TFCompairMethod newCompair = new TFCompairMethod(compairName,func, inputType,targetType);
            LabledItem newLabledItem = new LabledItem(newCompair, compairName, "TFCompair");
            dataController.AddItem(newLabledItem);
        }/*
        public static void AddAction(DataController dataController, string actionName, Action<object> func, System.Type inputType = null)
        {
            dataController.AddItem(
                new LabledItem(
                    new SMSAction(
                        actionName,
                        (object input) => func(input),
                        inputType: inputType,
                        haveInput: inputType != null),
                    actionName,
                    "Action"
                    ));
        }*/
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
