using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nino.StateMatching.Helper.Data
{

    [InlineEditor]
    public abstract class Item : StateMatchingScriptableObject
    {
        public string itemName;
        public string inGroup;
        public List<string> tags;
        protected override void InitializeScriptableObject()
        {
            itemName = "";
            inGroup = "";
            tags = new List<string>();
        }

    }
    [InlineEditor]
    public abstract class ItemCollection<Item> : StateMatchingScriptableObject where Item: Data.Item
    {
        public List<Item> items;
        protected override void InitializeScriptableObject()
        {
            items = new List<Item>();
        }

        public bool Contain(Predicate<Item> match) => DataUtility.ListContainItem(match, items);
        public Item GetItem(Predicate<Item> match) => DataUtility.GetItemInList(match, items);
        public List<Item> GetItems(Predicate<Item> match) => DataUtility.GetItemsInList(match, items);
        public bool AddItem(Item newItem) => DataUtility.AddItemToList(newItem, items);
        public int RemoveItems(Predicate<Item> match) => DataUtility.RemoveItemsInList(match, items);
    }

    [InlineEditor]
    public abstract class DataController<Item,ItemCollection> : StateMatchingScriptableObject 
        where Item: Data.Item
        where ItemCollection: Data.ItemCollection<Item>
    {
        [ReadOnly,LabelWidth(80)]public string dataType;
        [FoldoutGroup("Hint"),ReadOnly,TextArea(minLines:5,maxLines:20),SerializeField] string hint;
        [FoldoutGroup("Note"), TextArea(minLines: 5, maxLines: 20), SerializeField] string note;
        public ItemCollection collection;
        protected override void InitializeScriptableObject()
        {
            hint = WriteHint();
            note = "";
            collection = ScriptableObject.CreateInstance<ItemCollection>();
        }
        protected abstract string WriteHint();
        
    }

    [Serializable]
    public abstract class StateMatchingScriptableObject: ScriptableObject
    {
        bool initialized = false;
        private void OnEnable()
        {
            if (initialized) return;
            initialized = true;
            InitializeScriptableObject();
            InitializeInstance();
        }
        protected abstract void InitializeScriptableObject();
        protected abstract void InitializeInstance();
    }


    
}

