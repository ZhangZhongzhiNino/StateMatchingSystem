using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Nino.NewStateMatching
{
    [InlineEditor]
    public abstract class OldItem
    {
        [LabelWidth(80), PropertyOrder(-102),FoldoutGroup("Item Info")] public string itemName;
        [LabelWidth(80), PropertyOrder(-101), FoldoutGroup("Item Info")] public string group;
        [FoldoutGroup("Item Info/tags", Order = -100)] public List<string> tags;
        [FoldoutGroup("Item Info/tags"), Button,GUIColor(0.4f,1,0.4f),PropertyOrder(-1)] public void RemoveRedundantTags()
        {
            tags = DataUtility.RemoveAllRedundantStringInList(tags);
        }
        public bool ResetWenEnabled;
        public ItemValue value;
        public virtual void ResetData()
        {
            if (ResetWenEnabled) value = CreateNewValue();
        }
        public OldItem()
        {
            itemName = "";
            group = "";
            tags = new List<string>();
            value = CreateNewValue();
        }

        public abstract ItemValue CreateNewValue();
        public void InitiateValue() 
        {
            if(value == null) value = CreateNewValue();
        }
        public bool AddTag(string tag)
        {
            if (tags.Contains(tag)) return false;
            tags.Add(tag);
            return true;
        }
        public int AddTags(string[] _tags) 
        { 
            int i = 0;
            foreach (string s in _tags) if (AddTag(s)) i++;
            return i;
        }
        public int AddTags(List<string> _tags) => AddTags(_tags.ToArray());
        public bool RemoveTag(string tag)
        {
            if (!tags.Contains(tag)) return false;
            tags.Remove(tag);
            return true;
        }
        public int RemoveTags(string[] _tags)
        {
            int i = 0;
            foreach (string s in _tags) if (RemoveTag(s)) i++;
            return i;
        }
        public int RemoveTags(List<string> _tags) => RemoveTags(_tags.ToArray());
        public bool InGroup() => !string.IsNullOrWhiteSpace(group)&&!string.IsNullOrEmpty(group);
        public bool InGroup(string group) => this.group == group;
        public bool HaveTag()
        {
            return tags != null && tags.Count != 0;
        }
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
        public List<string> GetSameTags(OldItem _item) => GetSameTags(_item.tags);
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
        public List<string> HaveMoreTagsThan(OldItem _item) => HaveMoreTagsThan(_item.tags);
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
        public List<string> DontHaveTagsCompairTo(OldItem _item) => DontHaveTagsCompairTo(_item.tags);
        public static List<string> GetSameTags(OldItem item1, OldItem item2) => GetSameTags(new List<OldItem> { item1, item2 });
        public static List<string> GetSameTags(OldItem[] items) => GetSameTags(items.ToList());
        public static List<string> GetSameTags(List<OldItem> items)
        {
            List<string> r = new List<string>();
            Dictionary<string, int> tagCount = new Dictionary<string, int>();
            foreach (OldItem item in items)
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
        public static bool HaveSameTags(List<OldItem> items)
        {
            List<OldItem> compairItem = new List<OldItem>(items);
            int count = compairItem[0].tags.Count;
            List<string> tags0 = compairItem[0].tags;
            for (int i = 0; i < compairItem.Count; i++)
            {
                if (count != compairItem[i].tags.Count) return false;
                if (i != 0 && !compairItem[i].tags.OrderBy(x => x).SequenceEqual(tags0.OrderBy(x => x))) return false;
            }
            return true;
        }
        public static bool HaveSameTags(OldItem[] items) => HaveSameTags(items.ToList());
        public static bool HaveSameTags(OldItem A, OldItem B) => HaveSameTags(new List<OldItem> { A, B });
 
    }

    public abstract class ItemValue
    {
        public abstract void AssignValue(ItemValue newValue);
    }

    public class Item
    {
        [TitleGroup("Basic Info",order:-1),LabelWidth(140),PropertyOrder(0)] public string itemName;
        [TitleGroup("Basic Info"), LabelWidth(125), PropertyOrder(2),ReadOnly] public System.Type valueType;
        [TitleGroup("Basic Info"), LabelWidth(140), PropertyOrder(3)] public bool actionInput;
        [TitleGroup("Basic Info"), LabelWidth(140), PropertyOrder(4)] public bool resetWhenEnabled;
        [TitleGroup("Basic Info"), ShowIf("resetWhenEnabled"), LabelWidth(140), PropertyOrder(5)] public object defaultValue;

        [TitleGroup("Value")] public object value;

        
        public Item(System.Type valueType,string itemName)
        {
            this.itemName = itemName;
            this.valueType = valueType;
            value = Activator.CreateInstance(valueType);
            defaultValue = Activator.CreateInstance(valueType);
            resetWhenEnabled = false;
        }
        public Item(object value, string itemName)
        {
            this.itemName = itemName;
            this.valueType = value.GetType();
            this.value = value;
            defaultValue = Activator.CreateInstance(valueType);
        }
        public bool HaveSameTypeAs(object instance) => valueType.IsAssignableFrom(value.GetType());
        public bool IsType(System.Type type) => type == valueType;
        public T getValue<T>()
        {
            T r = default(T);
            if (typeof(T) == valueType) r = (T)value;
            return r;
        }
        public bool setValue<T>(T newValue)
        {
            if (IsType(typeof(T))) return false;
            this.value = newValue;
            return true;
        }
        public bool setValue(object newValue)
        {
            if (!HaveSameTypeAs(newValue)) return false;
            this.value = newValue;
            return true;
        }
        public void TryResetValue()
        {
            if (resetWhenEnabled)
            {
                value = Activator.CreateInstance(valueType);
                defaultValue.GetType().GetProperties().ToList().ForEach(x => x.SetValue(value, x.GetValue(defaultValue)));
            }
        }
        public void CreateValueIfNull()
        {
            if (value == null) value = Activator.CreateInstance(valueType);
            if (defaultValue == null) value = Activator.CreateInstance(valueType);
        }
    }
    public class LabledItem : Item
    {
        [TitleGroup("Basic Info"), LabelWidth(140), PropertyOrder(1)] public string group;
        [TitleGroup("tags")] public List<string> tags;
        public LabledItem(Type valueType, string itemName) : base(valueType, itemName)
        {
            group = "";
            tags = new List<string>();
        }
        public LabledItem(object value, string itemName) : base(value, itemName)
        {
            group = "";
            tags = new List<string>();
        }
        public LabledItem(Type valueType, string itemName,string group) : base(valueType: valueType, itemName)
        {
            this.group = group;
            tags = new List<string>();
        }
        public LabledItem(object value, string itemName,string group) : base(value, itemName)
        {
            this.group = group;
            tags = new List<string>();
        }

        public bool AddTag(string tag)
        {
            if (tags.Contains(tag)) return false;
            tags.Add(tag);
            return true;
        }
        public int AddTags(string[] _tags)
        {
            int i = 0;
            foreach (string s in _tags) if (AddTag(s)) i++;
            return i;
        }
        public int AddTags(List<string> _tags) => AddTags(_tags.ToArray());
        public bool RemoveTag(string tag)
        {
            if (!tags.Contains(tag)) return false;
            tags.Remove(tag);
            return true;
        }
        public int RemoveTags(string[] _tags)
        {
            int i = 0;
            foreach (string s in _tags) if (RemoveTag(s)) i++;
            return i;
        }
        public int RemoveTags(List<string> _tags) => RemoveTags(_tags.ToArray());
        public bool InGroup() => !string.IsNullOrWhiteSpace(group) && !string.IsNullOrEmpty(group);
        public bool InGroup(string group) => this.group == group;
        public bool HaveTag()
        {
            return tags != null && tags.Count != 0;
        }
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
            foreach (string t in _tags)
            {
                if (tags.Contains(t)) r.Add(t);
            }
            return new List<string>(r);
        }
        public List<string> GetSameTags(string[] _tags) => GetSameTags(_tags.ToList());
        public List<string> GetSameTags(OldItem _item) => GetSameTags(_item.tags);
        public List<string> HaveMoreTagsThan(List<string> _tags)
        {
            List<string> r = new List<string>();
            foreach (string s in tags)
            {
                if (!_tags.Contains(s)) r.Add(s);
            }
            return new List<string>(r);
        }
        public List<string> HaveMoreTagsThan(string[] _tags) => HaveMoreTagsThan(_tags.ToList());
        public List<string> HaveMoreTagsThan(OldItem _item) => HaveMoreTagsThan(_item.tags);
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
        public List<string> DontHaveTagsCompairTo(OldItem _item) => DontHaveTagsCompairTo(_item.tags);
        public static List<string> GetSameTags(OldItem item1, OldItem item2) => GetSameTags(new List<OldItem> { item1, item2 });
        public static List<string> GetSameTags(OldItem[] items) => GetSameTags(items.ToList());
        public static List<string> GetSameTags(List<OldItem> items)
        {
            List<string> r = new List<string>();
            Dictionary<string, int> tagCount = new Dictionary<string, int>();
            foreach (OldItem item in items)
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
        public static bool HaveSameTags(List<OldItem> items)
        {
            List<OldItem> compairItem = new List<OldItem>(items);
            int count = compairItem[0].tags.Count;
            List<string> tags0 = compairItem[0].tags;
            for (int i = 0; i < compairItem.Count; i++)
            {
                if (count != compairItem[i].tags.Count) return false;
                if (i != 0 && !compairItem[i].tags.OrderBy(x => x).SequenceEqual(tags0.OrderBy(x => x))) return false;
            }
            return true;
        }
        public static bool HaveSameTags(OldItem[] items) => HaveSameTags(items.ToList());
        public static bool HaveSameTags(OldItem A, OldItem B) => HaveSameTags(new List<OldItem> { A, B });

    }

    [InlineEditor]
    public class DataController:StateMatchingScriptableObject
    {
        [FoldoutGroup("Data"), PropertyOrder(1),DisableIf("@!editableInInspector")]
        [ListDrawerSettings(
            ListElementLabelName = "itemName",
            HideAddButton = true,
            HideRemoveButton = true,
            DraggableItems = false,
            ShowIndexLabels =true)] 
        public List<Item> items;
        [PropertyOrder(1),ShowIf("showDetailedItems") , DisableIf("@!editableInInspector"),FoldoutGroup("Detailed Reference")]
        [ListDrawerSettings(
            ListElementLabelName = "itemName",
            HideAddButton = true,
            HideRemoveButton = true,
            DraggableItems = false,
            ShowIndexLabels = true)]
        public List<Item> inputItems;
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
        public List<Item> labledItems;
        [FoldoutGroup("Quick Edit",order:10),PropertyOrder(-1),ShowIf("editableInInspector")] public Dictionary<string, Item> dic_unlabledItems; 
        [FoldoutGroup("Quick Edit"), ShowIf("editableInInspector")] public Dictionary<Type, List<Item>> dic_types;
        [FoldoutGroup("Quick Edit"), ShowIf("editableInInspector")] public Dictionary<string, List<LabledItem>> dic_groups;
        [FoldoutGroup("Quick Edit"), ShowIf("editableInInspector")] public Dictionary<string, List<LabledItem>> dic_tags;
        [PropertyOrder(20)] public bool showDetailedItems;
        [PropertyOrder(20)] public bool editableInInspector;

        [HorizontalGroup("Data/Null"), Button(ButtonSizes.Large), GUIColor(1, 0.4f, 0.4f)]
        void RemoveNullItems()
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
            foreach(LabledItem item in labledItems)
            {
                dic_types.TryAdd(item.valueType, new List<Item>());
                dic_types[item.valueType].Add(item);
                dic_groups.TryAdd(item.itemName, new List<LabledItem>());
                dic_groups[item.itemName].Add(item);
                foreach(string tag in item.tags)
                {
                    dic_tags.TryAdd(tag,new List<LabledItem>());
                    dic_tags[tag].Add(item);
                }
            }
        }

        protected override void RunOnEveryEnable()
        {
            if (items == null || items.Count == 0)
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
            inputItems = new List<Item>();
            unlabledItems = new List<Item>();
            labledItems = new List<Item>();
        }

        public void UpdateReferenceList()
        {
            RemoveNullItems();
            InitializeNullValues();
            UpdateInputItems();
            UpdateLabledItems();
            UpdateUnlabledItems();
        }
        public void UpdateInputItems() => inputItems = items.FindAll(x => x.actionInput == true);
        public void UpdateUnlabledItems() => unlabledItems = items.FindAll(x => x.GetType() == typeof(Item));
        public void UpdateLabledItems() => labledItems = items.FindAll(x => x.GetType() == typeof(LabledItem));

        public List<string> GetAllItemNames() => GeneralUtility.GetNameInItemList(items);
        public List<string> GetAllInputItemNames() => GeneralUtility.GetNameInItemList(inputItems);
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

        public bool Contain(Predicate<Item> match) => DataUtility.ListContainItem(match, items);

        public Item AddItem(string itemName, Type valueType, bool labled, string group)
        {
            if (labled && string.IsNullOrWhiteSpace(group)) return AddLabledItem(itemName, valueType);
            else if (labled) return AddLabledItem(itemName, group, valueType);
            else return AddItem(itemName, valueType);
        }
        public Item AddItem(Item newItem)
        {
            Item r = DataUtility.AddItemToList(newItem, items);
            UpdateReferenceList();
            return r;
        }
        public Item AddItem(string newItemName, Type valueType) => AddItem(new Item(valueType: valueType, newItemName));

        public LabledItem AddLabledItem(string newItemName, string groupName, Type valueType) => AddItem(new LabledItem(valueType: valueType, newItemName, groupName)) as LabledItem;
        public LabledItem AddLabledItem(string newItemName, Type valueType) => AddItem(new LabledItem(valueType: valueType, newItemName)) as LabledItem;

        
    }

}

