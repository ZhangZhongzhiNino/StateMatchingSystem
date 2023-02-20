using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.StateMatching.Helper.Data
{

    [InlineEditor]
    public abstract class Item : ScriptableObject, INamedDataComponent
    {
        public string itemName;
        public string componentName { get => itemName; set => itemName = value; }
        [ReadOnly] public int index;
        [ReadOnly] public List<Group<Item>> inGroups;

        
    }
    [InlineEditor]
    public class ItemColection<T> : ScriptableObject where T:Item
    {
        public List<T> items;
        public bool Contain(Predicate<T> match) => DataUtility.ListContainItem(match, items);
        public T GetItem(Predicate<T> match) => DataUtility.GetItemInList(match, items);
        public List<T> GetItems(Predicate<T> match) => DataUtility.GetItemsInList(match, items);
        public bool AddItem(T newItem) => DataUtility.AddItemToList(newItem, items);
        public int RemoveItems(Predicate<T> match) => DataUtility.RemoveItemsInList(match, items);
    }
    public class ItemController<T> : ScriptableObject where T : Item
    {
        ItemColection<T> itemCollection;
        public bool Contain(Predicate<T> match) => itemCollection.Contain(match);
        public T GetItem(Predicate<T> match) => itemCollection.GetItem(match);
        public List<T> GetItems(Predicate<T> match) => itemCollection.GetItems(match);
        public T AddItem(string newItemName)
        {
            T newItem = DataUtility.AddItemToList(newItemName, itemCollection.items);
            newItem.index = itemCollection.items.Count - 1;
            return newItem;
        }
        public int RemoveItems(Predicate<T> match) => itemCollection.RemoveItems(match);
    }
    [InlineEditor]
    public class Group<T> : ScriptableObject,INamedDataComponent where T : Item
    {
        public string GroupName;
        public string componentName { get => GroupName; set => GroupName = value; }
        ItemColection<T> itemCollection;
        public bool Contain(Predicate<T> match) => itemCollection.Contain(match);
        public T GetItem(Predicate<T> match) => itemCollection.GetItem(match);
        public List<T> GetItems(Predicate<T> match) => itemCollection.GetItems(match);
        public bool AddItem(T referenceItem) => itemCollection.AddItem(referenceItem);
        public int RemoveItems(Predicate<T> match) => itemCollection.RemoveItems(match);
    }
    [InlineEditor]
    public class GroupController<T> : ScriptableObject where T : Item
    {
        public List<Group<T>> groups;
        public bool Contain(Predicate<Group<T>> match) => DataUtility.ListContainItem(match, groups);
        public Group<T> GetGroup(Predicate<Group<T>> match) => DataUtility.GetItemInList(match, groups);
        public List<Group<T>> GetGroups(Predicate<Group<T>> match) => DataUtility.GetItemsInList(match, groups);
        public Group<T> AddGroup(string newGroupName) => DataUtility.AddItemToList(newGroupName, groups);
        public int RemoveGroups(Predicate<Group<T>> match) => DataUtility.RemoveItemsInList(match, groups);

    }
    [InlineEditor]
    public class DataController<T> : ScriptableObject,INamedDataComponent where T:Item
    {
        public string dataType;
        public string componentName { get => dataType; set => dataType = value; }
        public ItemColection<T> itemController;
        public GroupController<T> groupController;

        
    }

    public interface INamedDataComponent
    {
        public string componentName { get; set; }
    }
}

