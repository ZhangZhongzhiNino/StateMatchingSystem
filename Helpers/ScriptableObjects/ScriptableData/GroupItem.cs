using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;


namespace Nino.StateMatching.Helper.Data
{

    [InlineEditor]
    public abstract class Item : StateMatchingScriptableObject
    {
        [LabelWidth(80), PropertyOrder(-102)] public string itemName;
        [LabelWidth(80), PropertyOrder(-101)] public string inGroup;
        [FoldoutGroup("tags",Order = -100)] public List<string> tags;
        [FoldoutGroup("tags"), Button,GUIColor(0.4f,1,0.4f),PropertyOrder(-1)] public void RemoveRedundantTags()
        {
            tags = DataUtility.RemoveAllRedundantStringInList(tags);
        }

        protected override void InitializeScriptableObject()
        {
            itemName = "";
            inGroup = "";
            tags = new List<string>();
        }
        public void AssignItem(Item newItem, bool changeName =false, bool changeGroup = false, bool changeTags = false)
        {
            if (changeName) itemName = newItem.itemName;
            if (changeGroup) inGroup = newItem.inGroup;
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
        public bool InGroup() => !string.IsNullOrWhiteSpace(inGroup)&&!string.IsNullOrEmpty(inGroup);
        public bool InGroup(string group) => inGroup == group;
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
    public abstract class ItemCollection<Item> : StateMatchingScriptableObject where Item: Data.Item
    {
        [ListDrawerSettings(ShowIndexLabels = true,ListElementLabelName = "itemName")] public List<Item> items;
        [ListDrawerSettings(ShowIndexLabels = true),LabelWidth(50), GUIColor(0.9f, 0.9f, 1f)] public List<string> groups;
        [ListDrawerSettings(ShowIndexLabels = true), LabelWidth(50),GUIColor(0.8f,1,1f)] public List<string> tags;
        [Button,GUIColor(0.4f,1,0.4f),PropertyOrder(-1)]
        void InitiateAllNullDatas()
        {
            for(int i = 0; i < items.Count; i++)
            {
                if (items[i] == null) items[i] = ScriptableObject.CreateInstance<Item>();
            }
        }
        protected override void InitializeScriptableObject()
        {
            items = new List<Item>();
            groups = new List<string>();
            tags = new List<string>();
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
        public void UpdateTagListInCollection()
        {
            foreach(Item i in items)
            {
                if (i.HaveTag())
                {
                    tags = tags.Concat(i.HaveMoreTagsThan(tags)).ToList();
                }
            }
        }
        public List<string> GetAllGroupsInItems()
        {
            List<string> r = new List<string>();
            foreach (Item i in items)
            {
                if (i.InGroup() && !r.Contains(i.inGroup)) r.Add(i.inGroup);
            }
            return new List<string>(r);
        }
        public void UpdateGroupListInCollection()
        {
            foreach (Item i in items)
            {
                if (i.InGroup() && ! groups.Contains(i.inGroup)) groups.Add(i.inGroup);
            }
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
    public abstract class DataController<Item,ItemCollection> : StateMatchingScriptableObject 
        where Item: Data.Item
        where ItemCollection: Data.ItemCollection<Item>
    {
        
        [FoldoutGroup("Data Info"),ReadOnly,LabelWidth(80),PropertyOrder(-101)]public string dataType;
        [FoldoutGroup("Data Info")]public IDataExecuter<DataController<Item,ItemCollection>,Item,ItemCollection> attachedBehaviour;
        [FoldoutGroup("Hint",Order = -99),ReadOnly,TextArea(minLines:5,maxLines:20),SerializeField] string hint;
        [FoldoutGroup("Note",Order =-98), TextArea(minLines: 5, maxLines: 20), SerializeField] string note;
        [FoldoutGroup("Datas",Order=-97),PropertyOrder(-100)]public ItemCollection collection;

        #region Odin
        [FoldoutGroup("Datas/Save"), ReadOnly, LabelWidth(100),SerializeField,PropertyOrder(-99)] string savedPath;
        [FoldoutGroup("Datas/Save"),Button(Style = ButtonStyle.Box,ButtonHeight = 40),GUIColor(0.4f,1,0.4f)]
        public void SaveDataToFolder([FolderPath(RequireExistingPath = true)]string path = "Assets")
        {
            string rootPath = path;
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
        }


        [FoldoutGroup("Datas/Group"), Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f),PropertyOrder(-98)]
        void RemoveRedundantGroup()
        {
            collection.groups = DataUtility.RemoveAllRedundantStringInList(collection.groups);
        }
        [FoldoutGroup("Datas/Group"), Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)]
        void UpdateGroupList()
        {
            RemoveRedundantGroup();
            collection.UpdateGroupListInCollection();
        }
        [FoldoutGroup("Datas/Group"), Button(ButtonSizes.Large), GUIColor(1f, 0.4f, 0.4f)]
        void RemoveGroupsDontContainItem()
        {
            collection.groups = collection.GetAllGroupsInItems();
        }
        [FoldoutGroup("Datas/Group"), Button(Style = ButtonStyle.Box,ButtonHeight = 40), GUIColor(1f, 0.4f, 0.4f)]
        void RemoveGroup([ValueDropdown("@collection.groups")]string selectGroup,[LabelWidth(180)] bool removeContainedItem = false, [LabelWidth(180)] bool removeGroupInGroupList = true)
        {
            if (string.IsNullOrWhiteSpace(selectGroup)) return;
            if (removeGroupInGroupList) collection.groups.RemoveAll(x => x == selectGroup);
            if (removeContainedItem) collection.RemoveItems(x => x.inGroup == selectGroup);
            else
            {
                foreach(Item i in collection.items)
                {
                    if (i.inGroup == selectGroup) i.inGroup = "";
                }
            }
        }
        
        [FoldoutGroup("Datas/Tag"), Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f),PropertyOrder(-97)]
        void RemoveAllRedundantTags()
        {
            foreach (Item i in collection.items) i.RemoveRedundantTags();
            collection.tags = DataUtility.RemoveAllRedundantStringInList(collection.tags);
        }
        [FoldoutGroup("Datas/Tag"), Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)]
        void UpdateTagList()
        {
            RemoveAllRedundantTags();
            collection.UpdateTagListInCollection();
        }
        [FoldoutGroup("Datas/Tag"), Button(ButtonSizes.Large), GUIColor(1f, 0.4f, 0.4f)]
        void RemoveTagsNotAssignedToItem()
        {
            RemoveAllRedundantTags();
            collection.tags = collection.GetAllTagsInItems();
        }
        [FoldoutGroup("Datas/Tag"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(1f, 0.4f, 0.4f)]
        void RemoveTag([ValueDropdown("@collection.tags")]string selectTag ,[LabelWidth(180)] bool removeAttachedItem = false, [LabelWidth(180)] bool removeTagInTagList = true)
        {
            if (string.IsNullOrWhiteSpace(selectTag)) return;
            if (removeTagInTagList) collection.tags.RemoveAll(x=>x==selectTag);
            if (removeAttachedItem) collection.RemoveItems(x => x.HaveTag(selectTag));
            else
            {
                foreach(Item i in collection.items)
                {
                    i.RemoveTag(selectTag);
                }
            }
        }

        public enum FindItemMethod
        {
            group,
            tag
        }
        [FoldoutGroup("Datas/Item"), SerializeField,ListDrawerSettings(HideAddButton = true,ListElementLabelName = "itemName"),PropertyOrder(-96)] List<Item> temp_editItemList;
        [FoldoutGroup("Datas/Item/Find"), EnumToggleButtons, SerializeField] FindItemMethod findItemMethod;
        [FoldoutGroup("Datas/Item/Find"), ShowIf("@findItemMethod.ToString() == \"group\""), ValueDropdown("@collection.groups"),SerializeField] string temp_selectGroup;
        [FoldoutGroup("Datas/Item/Find"), ShowIf("@findItemMethod.ToString() == \"tag\""), ValueDropdown("@collection.tags"), SerializeField] string temp_selectTag;
        [FoldoutGroup("Datas/Item/Find"), Button(Style = ButtonStyle.Box,ButtonHeight =40),GUIColor(0.4f,1,0.4f)]
        void FindItems()
        {
            temp_editItemList = new List<Item>();
            if (findItemMethod == FindItemMethod.group) temp_editItemList = collection.GetItems(x => x.inGroup == temp_selectGroup);
            else if(findItemMethod == FindItemMethod.tag) temp_editItemList = collection.GetItems(x => x.HaveTag(temp_selectTag));
        }
        [FoldoutGroup("Datas/Item/Find"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void FindRepeatItem()
        {
            temp_editItemList = new List<Item>();
            List<string> names = new List<string>();
            foreach(Item i in collection.items)
            {
                if (names.Contains(i.itemName)) temp_editItemList = temp_editItemList.Concat(collection.GetItems(x => x.itemName == i.itemName)).ToList();
                else names.Add(i.itemName);
            }
        }

        #endregion
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

    public abstract class IDataExecuter<D,I,C>:MonoBehaviour
        where D:DataController<I,C>
        where I:Item
        where C:ItemCollection<I>
    {
        public List<D> dataControllers { get; set; }
    }
    
}

