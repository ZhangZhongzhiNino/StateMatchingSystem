﻿using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine.Events;
using UnityEngine;

namespace Nino.NewStateMatching
{
    [InlineEditor]
    public class DataController:StateMatchingScriptableObject
    {
        [FoldoutGroup("Data"), PropertyOrder(1),DisableIf("@!editableInInspector")]
        [ListDrawerSettings(
            ListElementLabelName = "itemName",
            HideAddButton = true, 
            DraggableItems = false,
            ShowIndexLabels =true)] 
        public List<Item> items;
        [PropertyOrder(1), ShowIf("showDetailedItems"), DisableIf("@!editableInInspector"), FoldoutGroup("Detailed Reference")]
        [ListDrawerSettings(
            ListElementLabelName = "itemName",
            HideAddButton = true,
            HideRemoveButton = true,
            DraggableItems = false,
            ShowIndexLabels = true)]
        public List<Item> unlabledItems;
        [PropertyOrder(1), ShowIf("showDetailedItems"), DisableIf("@!editableInInspector"), FoldoutGroup("Detailed Reference")]
        [ListDrawerSettings(
            ListElementLabelName = "itemName",
            HideAddButton = true,
            HideRemoveButton = true,
            DraggableItems = false,
            ShowIndexLabels = true)]
        public List<LabledItem> labledItems;
        [FoldoutGroup("Quick Edit",order:10),PropertyOrder(-1),ShowIf("showDetailedItems"), DisableIf("@!editableInInspector")] public Dictionary<string, Item> dic_unlabledItems; 
        [FoldoutGroup("Quick Edit"), ShowIf("showDetailedItems"), DisableIf("@!editableInInspector")] public Dictionary<Type, List<Item>> dic_types;
        [FoldoutGroup("Quick Edit"), ShowIf("showDetailedItems"), DisableIf("@!editableInInspector")] public Dictionary<string,List<LabledItem>> dic_groups;
        [FoldoutGroup("Quick Edit"), ShowIf("showDetailedItems"), DisableIf("@!editableInInspector")] public Dictionary<string, List<LabledItem>> dic_tags;
        [PropertyOrder(20)] public bool showDetailedItems;
        [PropertyOrder(20)] public bool editableInInspector;

        [HorizontalGroup("Data/Null"), Button(ButtonSizes.Large), GUIColor(1, 0.4f, 0.4f)]
        public void RemoveNullItems()
        {
            items.RemoveAll(x => x == null);
            items.RemoveAll(x => x.valueType == null);
            UpdateDictionrary();
        }
        void RemoveNullWiouthUpdate()
        {
            items.RemoveAll(x => x == null);
            items.RemoveAll(x => x.valueType == null);
        }
        [HorizontalGroup("Data/Null"), Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)] void InitializeNullValues() => items.ForEach(x => x.CreateValueIfNull());
        [FoldoutGroup("Data"),PropertyOrder(1), Button(Style = ButtonStyle.Box,ButtonHeight = 40), GUIColor(1, 0.4f, 0.4f), ShowIf("@editableInInspector")]
        void RemoveItemById(int index) 
        { 
            if (index < items.Count) items.RemoveAt(index);
            UpdateReferenceList();
        }
        [FoldoutGroup("Data"), PropertyOrder(1), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(1, 0.4f, 0.4f), ShowIf("@editableInInspector")]
        void RemoveItemByName([ValueDropdown("@GetAllItemNames()")] string name)
        {
            items.RemoveAll(x => x.itemName == name);
            UpdateReferenceList();
        }


        
        [FoldoutGroup("Quick Edit"),Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f), ShowIf("editableInInspector")] void UpdateDictionrary()
        {
            UpdateReferenceList();
            dic_unlabledItems = new Dictionary<string, Item>();
            dic_types = new Dictionary<Type, List<Item>>();
            dic_groups = new Dictionary<string, List<LabledItem>>();
            dic_tags = new Dictionary<string, List<LabledItem>>();
            foreach (Item item in unlabledItems)
            {
                dic_unlabledItems.TryAdd(item.itemName, item);
                dic_types.TryAdd(item.valueType, new List<Item>());
                dic_types[item.valueType].Add(item);
            }
            foreach (LabledItem item in labledItems)
            {
                dic_types.TryAdd(item.valueType, new List<Item>());
                dic_types[item.valueType].Add(item);
                dic_groups.TryAdd(item.group, new List<LabledItem>());
                dic_groups[item.group].Add(item);
                foreach(string tag in item.tags)
                {
                    dic_tags.TryAdd(tag,new List<LabledItem>());
                    dic_tags[tag].Add(item);
                }
            }
        }

        protected override void RunOnEveryEnable()
        { 
            if (items != null || items.Count != 0)
            {
                items.ForEach(x => x.TryResetValue());
                return;
            }
            UpdateReferenceList();
            UpdateDictionrary();
        }
        protected override void Initialize()
        {
            editableInInspector = false;
            items = new List<Item>();
            unlabledItems = new List<Item>();
            labledItems = new List<LabledItem>();
            UpdateDictionrary();
            UpdateReferenceList();
        }

        public void UpdateReferenceList()
        {
            RemoveNullWiouthUpdate();
            InitializeNullValues();
            UpdateLabledItems();
            UpdateUnlabledItems();
        }
        public void UpdateUnlabledItems() => unlabledItems = items.FindAll(x => x.GetType() == typeof(Item));
        public void UpdateLabledItems()
        {
            labledItems = new List<LabledItem>();
            foreach(Item item in items)
            {
                if(item is LabledItem labledItem)
                {
                    labledItems.Add(labledItem);
                }
            }
        }

        public List<string> GetAllItemNames() => GeneralUtility.GetNameInItemList(items);
        public List<string> GetAllIUnlabledtemNames() => GeneralUtility.GetNameInItemList(unlabledItems);
        public List<string> GetAllILabledtemNames() => GeneralUtility.GetNameInItemList(labledItems);
        public List<string> GetAllItemNamesOfType(Type valueType)
        {
            if (dic_types.Keys.Contains(valueType)) 
                return GeneralUtility.GetNameInItemList(dic_types[valueType]);
            return new List<string>();
        }
        public List<string> GetAllItemNamesOfGroup(string group)
        {
            if (dic_groups.Keys.Contains(group))
                return GeneralUtility.GetNameInItemList(dic_groups[group]);
            return new List<string>();
        }
        public List<string> GetAllItemNamesOfTag(string tag)
        {
            if (dic_tags.Keys.Contains(tag))
                return GeneralUtility.GetNameInItemList(dic_tags[tag]);
            return new List<string>();
        }
        public Item GetItem(string itemName) => items.FirstOrDefault(x => x.itemName == itemName);
        public LabledItem GetLabledItem(string itemName, string groupName = null, List<string> lables = null)
        {
            foreach(LabledItem item in labledItems)
            {
                if(itemName == item.itemName)
                {
                    if (groupName != null && groupName != item.group) continue;
                    if (lables != null && lables.Count != 0 && !item.HaveTags(lables)) continue;
                    return item;
                }
            }
            return null;
        }
        public bool Contain(Predicate<Item> match) => DataUtility.ListContainItem(match, items);

        public Item AddItem(string itemName, Type valueType, bool labled, string group,bool useOdinSerialization = true)
        {
            if (labled && string.IsNullOrWhiteSpace(group)) return AddLabledItem(itemName, valueType, useOdinSerialization);
            else if (labled) return AddLabledItem(itemName, group, valueType, useOdinSerialization);
            else return AddItem(itemName, valueType, useOdinSerialization);
        }
        public Item AddItem(Item newItem)
        {
            Item r = DataUtility.AddItemToList(newItem, items);
            UpdateDictionrary();
            UpdateReferenceList();
            return r;
        }
        public Item AddItem(string newItemName, Type valueType, bool useOdinSerialization = true) => AddItem(new Item(valueType: valueType, newItemName, useOdinSerialization));

        public LabledItem AddLabledItem(string newItemName, string groupName, Type valueType, bool useOdinSerialization = true) => AddItem(new LabledItem(valueType: valueType, newItemName, groupName, useOdinSerialization)) as LabledItem;
        public LabledItem AddLabledItem(string newItemName, Type valueType, bool useOdinSerialization = true) => AddItem(new LabledItem(valueType: valueType, newItemName, useOdinSerialization)) as LabledItem;

        public void ClearItemsButNotActionAndCompairMethods()
        {
            items.RemoveAll(x =>
            {
                if (x is LabledItem labledItem)
                {
                    if (labledItem.group == "Compair") return false;
                    if (labledItem.group == "TFCompair") return false;
                    if (labledItem.group == "Action") return false;
                }
                return true;
            });
            RemoveNullItems();
        }
        
    }
}

