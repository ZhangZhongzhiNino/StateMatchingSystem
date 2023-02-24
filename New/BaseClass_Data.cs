using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;



namespace Nino.NewStateMatching
{

    [InlineEditor]
    public abstract class Item : BaseScriptableObject
    {
        [LabelWidth(80), PropertyOrder(-102)] public string itemName;
        [LabelWidth(80), PropertyOrder(-101)] public string group;
        [FoldoutGroup("tags",Order = -100)] public List<string> tags;
        [FoldoutGroup("tags"), Button,GUIColor(0.4f,1,0.4f),PropertyOrder(-1)] public void RemoveRedundantTags()
        {
            tags = DataUtility.RemoveAllRedundantStringInList(tags);
        }
        
        protected override void InitializeBaseScriptableObject()
        {
            itemName = "";
            group = "";
            tags = new List<string>();
        }
        public void AssignItem(Item newItem, bool changeName =false, bool changeGroup = false, bool changeTags = false)
        {
            if (changeName) itemName = newItem.itemName;
            if (changeGroup) group = newItem.group;
            if (changeTags) tags = new List<string>(newItem.tags);
            AssignItem(newItem);
        }
        protected abstract void AssignItem(Item newItem);
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
    public abstract class Collection<Item> : BaseScriptableObject where Item: NewStateMatching.Item
    {
        [FoldoutGroup("Items",order: -100),ListDrawerSettings(ShowIndexLabels = true,ListElementLabelName = "itemName"),PropertyOrder(1),PropertySpace(SpaceBefore =2,SpaceAfter =10)] public List<Item> items;
        [FoldoutGroup("Group and Tags"),TabGroup("Group and Tags/veriables", "Groups"),ReadOnly, PropertyOrder(-1)] public Dictionary<string,int> groupQuickLook;
        [TabGroup("Group and Tags/veriables", "Tags"), ReadOnly,PropertyOrder(-1), SerializeField] public Dictionary<string, int> tagQuickLook;

        public List<string> groups { get => groupQuickLook?.Keys?.ToList(); }
        public List<string> tags { get => tagQuickLook?.Keys?.ToList(); }

        protected override void InitializeBaseScriptableObject()
        {
            items = new List<Item>();
            groupQuickLook = new Dictionary<string, int>();
            tagQuickLook = new Dictionary<string, int>();
        }


        public enum FindItemMethod
        {
            group,
            tag
        }
        public enum FindBool
        {
            ContainAll,
            ContainAny
        }
        [TabGroup("Group and Tags/veriables", "Items"), SerializeField, ListDrawerSettings(HideAddButton = true, ListElementLabelName = "itemName"), PropertyOrder(-2),PropertySpace(SpaceBefore =5,SpaceAfter =5)] List<Item> temp_editItemList;
        [FoldoutGroup("Group and Tags/veriables/Items/Search By Tags or Groups"), EnumToggleButtons, SerializeField] FindItemMethod findItemMethod;
        [FoldoutGroup("Group and Tags/veriables/Items/Search By Tags or Groups"), ShowIf("@findItemMethod.ToString() == \"group\""), ValueDropdown("@groups"), SerializeField] string temp_selectGroup;
        [FoldoutGroup("Group and Tags/veriables/Items/Search By Tags or Groups"), ShowIf("@findItemMethod.ToString() == \"tag\""), EnumToggleButtons, SerializeField] FindBool searchBool;
        [FoldoutGroup("Group and Tags/veriables/Items/Search By Tags or Groups"), ShowIf("@findItemMethod.ToString() == \"tag\""), ValueDropdown("@OdinUtility.ValueDropDownListSelector(tags,temp_selectTag)"), SerializeField] List<string> temp_selectTag;
        [FoldoutGroup("Group and Tags/veriables/Items/Search By Tags or Groups"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]void FindItems()
        {
            InitiateNullDatas();
            temp_editItemList = new List<Item>();
            if (findItemMethod == FindItemMethod.group) temp_editItemList = GetItems(x => x.group == temp_selectGroup);
            else if (findItemMethod == FindItemMethod.tag && searchBool == FindBool.ContainAll) temp_editItemList = GetItems(x => x.HaveTags(temp_selectTag));
            else 
            {
                foreach (string tag in temp_selectTag) 
                {
                    temp_editItemList = temp_editItemList.Concat(GetItems(x => x.HaveTag(tag))).ToList();
                }
                temp_editItemList = temp_editItemList.Distinct().ToList();
            }
        }
        [FoldoutGroup("Group and Tags/veriables/Items/Other Search Method"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)] void FindRepeatItem()
        {
            InitiateNullDatas();
            temp_editItemList = new List<Item>();
            List<string> names = new List<string>();
            foreach (Item i in items)
            {
                if (names.Contains(i.itemName)) temp_editItemList = temp_editItemList.Concat(GetItems(x => x.itemName == i.itemName)).ToList();
                else names.Add(i.itemName);
            }
        }
        [FoldoutGroup("Group and Tags/veriables/Items/Other Search Method"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.7f, 1, 0.7f)] void FindUnNamedItem()
        {
            InitiateNullDatas();
            temp_editItemList = GetItems(x => string.IsNullOrEmpty(x.itemName) || string.IsNullOrWhiteSpace(x.itemName));
        }
        [FoldoutGroup("Group and Tags/veriables/Items/Other Search Method"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)] void FindUnGroupedItem()
        {
            InitiateNullDatas();
            temp_editItemList = GetItems(x => !x.InGroup());
        }
        [FoldoutGroup("Group and Tags/veriables/Items/Other Search Method"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.7f, 1, 0.7f)] void FindUnTagedItem()
        {
            InitiateNullDatas();
            temp_editItemList = GetItems(x =>x.tags == null ||x.tags.Count == 0);
        }

        [FoldoutGroup("Items"), Button(ButtonSizes.Large), GUIColor(1f, 1f, 0.4f), PropertyOrder(-2)] public void RemoveRedundantTagsInAllItems()
        {
            foreach (Item i in items) i.RemoveRedundantTags();
        }
        [HorizontalGroup("Items/DataEditButton"), Button(ButtonSizes.Large), GUIColor(1f, 0.4f, 0.4f), PropertyOrder(-1)] void RemoveRedundantDatas()
        {
            items.RemoveAll(x => x == null || x == default(Item) || string.IsNullOrWhiteSpace(x.itemName));
            List<string> names = new List<string>();
            List<Item> toRemove = new List<Item>();
            foreach (Item i in items)
            {
                if (names.Contains(i.itemName)) toRemove.Add(i);
                else names.Add(i.itemName);
            }
            foreach (Item i in toRemove)
            {
                items.Remove(i);
            }

        }
        [HorizontalGroup("Items/DataEditButton"),Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)] void InitiateNullDatas()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == null) items[i] = ScriptableObject.CreateInstance<Item>();
            }
        }


        [TabGroup("Group and Tags/veriables", "Groups"), Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f), PropertyOrder(-1)] public void UpdateGroupDictionary()
        {
            List<string> groups = GetAllGroupsInItems();
            groupQuickLook = new Dictionary<string, int>();
            foreach (string s in groups) groupQuickLook.Add(s, 0);
            foreach (Item i in items)
            {
                if (groupQuickLook.ContainsKey(i.group)) groupQuickLook[i.group]++;
            }
        }
        [TabGroup("Group and Tags/veriables", "Groups"), Button(ButtonHeight = 40,Style = ButtonStyle.Box), GUIColor(1f, 0.4f, 0.4f)] 
        void RemoveGroup([ValueDropdown("@groupDictionary.Keys")] string selectGroup, [LabelWidth(180)] bool removeContainedItem = true)
        {
            if (string.IsNullOrWhiteSpace(selectGroup)) return;
            if (removeContainedItem) RemoveItems(x => x.group == selectGroup);
            else
            {
                foreach (Item i in items)
                {
                    if (i.group == selectGroup) i.group = "";
                }
            }
            UpdateGroupDictionary();
        }
        [TabGroup("Group and Tags/veriables", "Groups"), Button(ButtonSizes.Large, Style = ButtonStyle.Box), GUIColor(0.4f, 1, 0.4f)]
        void RenameGroup([ValueDropdown("groups")] string selectGroup,string newName)
        {
            List<Item> getItemMatch = GetItems(x => x.InGroup(selectGroup));
            foreach (Item i in getItemMatch) i.group = newName;
            UpdateGroupDictionary();
        }

        [TabGroup("Group and Tags/veriables", "Tags"), Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f),PropertyOrder(-1)] public void UpdateTagQuickLook()
        {
            RemoveRedundantTagsInAllItems();
            List<string> tags = GetAllTagsInItems();
            tagQuickLook = new Dictionary<string, int>();
            foreach (string s in tags) tagQuickLook.Add(s, 0);
            foreach (Item i in items)
            {
                foreach (string tag in i.tags) if (tagQuickLook.ContainsKey(tag)) tagQuickLook[tag]++;
            }
        }
        [FoldoutGroup("Group and Tags/veriables/Tags/RemoveTag"), ValueDropdown("@OdinUtility.ValueDropDownListSelector(tags,selectTag)"),SerializeField] List<string> selectTag;
        [FoldoutGroup("Group and Tags/veriables/Tags/RemoveTag"), LabelWidth(180), SerializeField] bool removeAttachedItem = false;
        [FoldoutGroup("Group and Tags/veriables/Tags/RemoveTag"), Button(ButtonHeight = 40), GUIColor(1f, 0.4f, 0.4f)] void RemoveTag( )
        {
            if (removeAttachedItem) RemoveItems(x => x.HaveTags(selectTag));
            else
            {
                foreach (Item i in items)
                {
                    i.RemoveTags(selectTag);
                }
            }
            UpdateTagQuickLook();
        }
        [TabGroup("Group and Tags/veriables", "Tags"), Button(ButtonSizes.Large,Style = ButtonStyle.Box), GUIColor(0.4f, 1, 0.4f)]
        void RenameTag([ValueDropdown("tags")]string selectTag, string newName)
        {
            List<Item> getMatchItems = GetItems(x => x.HaveTag(selectTag));
            foreach (Item i in getMatchItems)
            {
                i.RemoveTag(selectTag);
                i.AddTag(newName);
            }
            UpdateTagQuickLook();
        }


        public List<string> GetAllItemNames()
        {
            List<string> r = new List<string>();
            foreach (Item i in items)
            {
                if (!r.Contains(i.itemName)) r.Add(i.itemName);
            }
            return new List<string>(r);
        }
        public List<string> GetAllTagsInItems()
        {
            List<string> r = new List<string>();
            foreach(Item i in items)
            {
                if (i.HaveTag())
                {
                    r = r.Concat(i.HaveMoreTagsThan(r)).ToList();
                }
            }
            return new List<string>(r);
        }
        public List<string> GetAllGroupsInItems()
        {
            List<string> r = new List<string>();
            foreach (Item i in items)
            {
                if (i.InGroup() && !r.Contains(i.group)) r.Add(i.group);
            }
            return new List<string>(r);
        }
        public bool Contain(Predicate<Item> match) => DataUtility.ListContainItem(match, items);
        public Item GetItem(Predicate<Item> match) => DataUtility.GetItemInList(match, items);
        public Item GetItemCopy(Predicate<Item> match) => DataUtility.CopyScriptableObject<Item>(DataUtility.GetItemInList(match, items));
        public List<Item> GetItems(Predicate<Item> match) => DataUtility.GetItemsInList(match, items);
        public List<Item> GetItemsCopy(Predicate<Item> match)
        {
            List<Item> getItems = DataUtility.GetItemsInList(match, items);
            List<Item> r = new List<Item>();
            foreach(Item i in getItems)
            {
                r.Add(DataUtility.CopyScriptableObject<Item>(i));
            }
            return r;
        }
        public bool AddItem(Item newItem) => DataUtility.AddItemToList(newItem, items);
        public bool AddItemCopy(Item newItem)
        {
            Item addItem = DataUtility.CopyScriptableObject<Item>(newItem);
            return DataUtility.AddItemToList(addItem, items);
        }
        public bool AddItem(string newItemName) => DataUtility.AddItemToList(newItemName, items);
        public int RemoveItems(Predicate<Item> match) => DataUtility.RemoveItemsInList(match, items);
    }

    [InlineEditor]
    public abstract class DataController<Item,ItemCollection> : BaseScriptableObject 
        where Item: NewStateMatching.Item
        where ItemCollection: NewStateMatching.Collection<Item>
    {
        [ReadOnly,LabelWidth(80),PropertyOrder(-101)]public string dataType;
        [FoldoutGroup("Hint",Order = -99),TextArea(minLines:5,maxLines:20),SerializeField] string hint;
        [FoldoutGroup("Note",Order =-98), TextArea(minLines: 5, maxLines: 20), SerializeField] string note;
        [FoldoutGroup("Data",Order=-97),PropertyOrder(-100),PropertySpace(SpaceAfter = 20, SpaceBefore = 10)]public ItemCollection collection;


        [FoldoutGroup("Data Output Reference",Order = -96)] public Dictionary<string, Item> dic_items;
        [FoldoutGroup("Data Output Reference")] public Dictionary<string, List<Item>> dic_groups;
        [FoldoutGroup("Data Output Reference")] public Dictionary<string, List<Item>> dic_tags;
        [FoldoutGroup("Data Output Reference"),Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)] void UpdateReferences()
        {
            collection.RemoveRedundantTagsInAllItems();
            dic_items = new Dictionary<string, Item>();
            dic_groups = new Dictionary<string, List<Item>>();
            dic_tags = new Dictionary<string, List<Item>>();
            foreach(Item item in collection.items)
            {
                if (!dic_items.Keys.Contains(item.itemName)) dic_items.Add(item.itemName, item);
                if (!dic_groups.Keys.Contains(item.group)) dic_groups.Add(item.group, new List<Item>());
                dic_groups[item.group].Add(item);
                foreach(string tag in item.tags)
                {
                    if (!dic_tags.Keys.Contains(tag)) dic_tags.Add(tag, new List<Item>());
                    dic_tags[tag].Add(item);
                }
            }
        }


        [FoldoutGroup("Save",Order = -95), FolderPath(RequireExistingPath = true), LabelWidth(100),SerializeField , PropertySpace(SpaceBefore = 10)] string savedPath = "Assets";
        [FoldoutGroup("Save"),Button(Style = ButtonStyle.Box,ButtonHeight = 40),GUIColor(0.4f,1,0.4f), PropertySpace(SpaceAfter = 20)] public void SaveDataToFolder()
        {
            string rootPath = savedPath;
            if (!AssetDatabase.Contains(this))
            {
                if (AssetUtility.CreateFolder(rootPath, dataType)) rootPath = rootPath + "/" + dataType;
                else throw new Exception("Please give a valid address");
            }
            if(!AssetUtility.SaveAsset(this, rootPath + "/" + dataType + "_Controller.asset"))
            {
                rootPath = AssetDatabase.GetAssetPath(this);
                rootPath = System.IO.Path.GetDirectoryName(rootPath);
            }
            AssetUtility.SaveAsset(collection, rootPath + "/" + dataType + "_DataCollection.asset");
            if (AssetUtility.CreateFolder(rootPath, "Datas")) rootPath = rootPath + "/Datas";
            else throw new Exception("Path Error");
            foreach (Item i in collection.items)
            {
                AssetUtility.SaveAsset(i, rootPath + "/" + i.itemName + ".asset");
            }
            savedPath = AssetDatabase.GetAssetPath(this);
            UnityEditor.EditorGUIUtility.PingObject(this);
            savedPath = System.IO.Path.GetDirectoryName(rootPath);
            
        }


        protected override void InitializeBaseScriptableObject()
        {
            dataType = WriteDataType();
            string BasicHint = "\n\n______________________\n" +
                "\nDataController Explain:" +
                "\nThis scriptable object hold " + dataType + " type of data." +
                "\nYou can |create|edit|remove| data in this section." +
                "\n\nDataController Sections:" +
                "\nNote: This section will not compail, It's only for you to take note." +
                "\n\nData: Section to edit data." +
                "\nData_Items: Section to edit all data" +
                "\n\nData_Group and Tags_Items: Different find methods will put different items in temp list, you can edit items in temp list. " +
                "\nData_Group and Tags_Groups || Tags: The place to remove or rename groups or tags. You can get basic information about groups or tags in QuickLook." +
                "\n\nData Output Reference: Dictionaries hold references to data and are sorted by item name, item group, and item tags. This is a helper section for accessing the data. You could also edit data at this section. It is important to update references at this section after change." +
                "\n\nSave: The place to save data to the folder. If data is already saved to the folder this will save instance changes to the scriptable object in the folder.";
            hint = WriteHint() + BasicHint;
            note = "";
            collection = ScriptableObject.CreateInstance<ItemCollection>();
            dic_items = new Dictionary<string, Item>();
            dic_groups = new Dictionary<string, List<Item>>();
            dic_tags = new Dictionary<string, List<Item>>();
        }
        protected abstract string WriteHint();
        protected abstract string WriteDataType();

        
    }

    [Serializable]
    public abstract class StateMatchingScriptableObject : SerializedScriptableObject
    {
        bool initialized = false;
        private void OnEnable()
        {
            if (initialized) return;
            initialized = true;
            Initialize();
        }
        protected abstract void Initialize();

    }


    public abstract class BaseScriptableObject : StateMatchingScriptableObject
    {
        protected override void Initialize()
        {
            InitializeBaseScriptableObject();
            InitializeInstance();
        }
        protected abstract void InitializeBaseScriptableObject();
        protected abstract void InitializeInstance();

    }
}

