using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;
using System.Linq;

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

        public bool InGroup() => string.IsNullOrEmpty(inGroup);
        public bool InGroup(string group) => inGroup == group;
        public bool HaveTag(string tag) => tags.Contains(tag);
        public bool HaveTags(List<string> _tags)
        {
            foreach (string tag in _tags) if (!tags.Contains(tag)) return false;
            return true;
        }
        public bool HaveTags(string[] _tags) => HaveTags(_tags.ToList());
        public List<string> GetSameTags(List<string> _tags)
        {
            List<string> r = new List<string>();
            foreach(string t in _tags)
            {
                if (tags.Contains(t)) r.Add(t);
            }
            return new List<string>(r);
        }
        public List<string> GetSameTags(string[] _tags) => GetSameTags(_tags.ToList());
        public List<string> GetSameTags(Item _item) => GetSameTags(_item.tags);
        public List<string> HaveMoreTagsThan(List<string> _tags)
        {
            List<string> r = new List<string>();
            foreach(string s in tags)
            {
                if (!_tags.Contains(s)) r.Add(s);
            }
            return new List<string>(r);
        }
        public List<string> HaveMoreTagsThan(string[] _tags) => HaveMoreTagsThan(_tags.ToList());
        public List<string> HaveMoreTagsThan(Item _item) => HaveMoreTagsThan(_item.tags);
        public List<string> DontHaveTagsCompairTo(List<string> _tags)
        {
            List<string> r = new List<string>();
            foreach (string s in _tags)
            {
                if (!tags.Contains(s)) r.Add(s);
            }
            return new List<string>(r);
        }
        public List<string> DontHaveTagsCompairTo(string[] _tags) => DontHaveTagsCompairTo(_tags.ToList());
        public List<string> DontHaveTagsCompairTo(Item _item) => DontHaveTagsCompairTo(_item.tags);
        public static List<string> GetSameTags(Item item1, Item item2) => GetSameTags(new List<Item> { item1, item2 });
        public static List<string> GetSameTags(Item[] items) => GetSameTags(items.ToList());
        public static List<string> GetSameTags(List<Item> items)
        {
            List<string> r = new List<string>();
            Dictionary<string, int> tagCount = new Dictionary<string, int>();
            foreach (Item item in items)
            {
                foreach (string s in item.tags)
                {
                    if (tagCount.ContainsKey(s)) tagCount[s]++;
                    else tagCount[s] = 1;
                }
            }
            foreach (KeyValuePair<string, int> pair in tagCount)
            {
                if (pair.Value == items.Count) r.Add(pair.Key);
            }
            return new List<string>(r);
        }
        public static bool HaveSameTags(List<Item> items)
        {
            List<Item> compairItem = new List<Item>(items);
            int count = compairItem[0].tags.Count;
            List<string> tags0 = compairItem[0].tags;
            for (int i = 0; i < compairItem.Count; i++)
            {
                if (count != compairItem[i].tags.Count) return false;
                if (i != 0 && !compairItem[i].tags.OrderBy(x => x).SequenceEqual(tags0.OrderBy(x => x))) return false;
            }
            return true;
        }
        public static bool HaveSameTags(Item[] items) => HaveSameTags(items.ToList());
        public static bool HaveSameTags(Item A, Item B) => HaveSameTags(new List<Item> { A, B });
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

