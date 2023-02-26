using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;



namespace Nino.NewStateMatching
{
    [InlineEditor]
    public abstract class NewDataController<Item,ItemCollection> : DataScriptableObject
        where Item: NewStateMatching.Item,new()
        where ItemCollection: Collection<Item>, new()
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
            /*AssetUtility.SaveAsset(collection, rootPath + "/" + dataType + "_DataCollection.asset");
            if (AssetUtility.CreateFolder(rootPath, "Datas")) rootPath = rootPath + "/Datas";
            else throw new Exception("Path Error");
            foreach (Item i in collection.items)
            {
                AssetUtility.SaveAsset(i, rootPath + "/" + i.itemName + ".asset");
            }*/
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
            collection = new ItemCollection();
            dic_items = new Dictionary<string, Item>();
            dic_groups = new Dictionary<string, List<Item>>();
            dic_tags = new Dictionary<string, List<Item>>();
        }
        protected abstract string WriteHint();
        protected abstract string WriteDataType();

        
    }
    [InlineEditor,Serializable]
    public abstract class DataController : DataScriptableObject
    {
        [ReadOnly, LabelWidth(80), PropertyOrder(-101)] public string dataType;
        [FoldoutGroup("Hint", Order = -99), TextArea(minLines: 5, maxLines: 20), SerializeField] string hint;
        [FoldoutGroup("Note", Order = -98), TextArea(minLines: 5, maxLines: 20), SerializeField] string note;
        //[FoldoutGroup("Data", Order = -97), PropertyOrder(-100), PropertySpace(SpaceAfter = 20, SpaceBefore = 10), SerializeField] public  List<Item> items;
        public abstract List<Item> GetItemsFromInstance();
        [FoldoutGroup("Data"), Button(ButtonSizes.Large), GUIColor(1f, 1f, 0.4f), PropertyOrder(-2)]
        public void RemoveRedundantTagsInAllItems()
        {
            List<Item> items = GetItemsFromInstance();
            foreach (Item i in items) i.RemoveRedundantTags();
        }
        [FoldoutGroup("Data"), Button(ButtonSizes.Large), GUIColor(1f, 0.4f, 0.4f), PropertyOrder(-1)]
        void RemoveRedundantDatas()
        {
            List<Item> items = GetItemsFromInstance();
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

        [FoldoutGroup("Data Output Reference",Order = -96), SerializeField] public Dictionary<string, Item> dic_items;
        [FoldoutGroup("Data Output Reference"), SerializeField] public Dictionary<string, List<Item>> dic_groups;
        [FoldoutGroup("Data Output Reference"), SerializeField] public Dictionary<string, List<Item>> dic_tags;
        [FoldoutGroup("Data Output Reference"), Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)]
        void UpdateReferences()
        {
            List<Item> items = GetItemsFromInstance();
            RemoveRedundantTagsInAllItems();
            dic_items = new Dictionary<string, Item>();
            dic_groups = new Dictionary<string, List<Item>>();
            dic_tags = new Dictionary<string, List<Item>>();
            foreach (Item item in items)
            {
                if (!dic_items.Keys.Contains(item.itemName)) dic_items.Add(item.itemName, item);
                if(item.group != null)
                {
                    if (!dic_groups.Keys.Contains(item.group)) dic_groups.Add(item.group, new List<Item>());
                    dic_groups[item.group].Add(item);
                }
                
                foreach (string tag in item.tags)
                {
                    if (!dic_tags.Keys.Contains(tag)) dic_tags.Add(tag, new List<Item>());
                    dic_tags[tag].Add(item);
                }
            }
        }


        [FoldoutGroup("Save", Order = -94), FolderPath(RequireExistingPath = true), LabelWidth(100), SerializeField, PropertySpace(SpaceBefore = 10)] string savedPath = "Assets";
        [FoldoutGroup("Save"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f), PropertySpace(SpaceAfter = 20)]
        public void SaveDataToFolder()
        {
            string rootPath = savedPath;
            if (!AssetDatabase.Contains(this))
            {
                AssetUtility.CreateFolder(rootPath, dataType);
                AssetDatabase.CreateAsset(this, rootPath + "/" + dataType + "/" + dataType + "_DataController.asset");
                
            }
            else
            {
                AssetDatabase.SaveAssetIfDirty(this);
                Debug.Log("saved");
            }
            /*AssetUtility.SaveAsset(collection, rootPath + "/" + dataType + "_DataCollection.asset");
            if (AssetUtility.CreateFolder(rootPath, "Datas")) rootPath = rootPath + "/Datas";
            else throw new Exception("Path Error");
            foreach (Item i in collection.items)
            {
                AssetUtility.SaveAsset(i, rootPath + "/" + i.itemName + ".asset");
            }*/
            UnityEditor.EditorGUIUtility.PingObject(this);
            AssetDatabase.SaveAssets();
        }


        protected override void InitializeBaseScriptableObject()
        {
            List<Item> items = GetItemsFromInstance();
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
            items = new List<Item>();
            dic_items = new Dictionary<string, Item>();
            dic_groups = new Dictionary<string, List<Item>>();
            dic_tags = new Dictionary<string, List<Item>>();
        }
        protected abstract string WriteHint();
        protected abstract string WriteDataType();

        void InitiateNullDatas()
        {
            List<Item> items = GetItemsFromInstance();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == null) items[i] = (Item)Activator.CreateInstance(typeof(Item));
            }
        }
        public List<string> GetAllItemNames()
        {
            List<Item> items = GetItemsFromInstance();
            List<string> r = new List<string>();
            foreach (Item i in items)
            {
                if (!r.Contains(i.itemName)) r.Add(i.itemName);
            }
            return new List<string>(r);
        }
        public List<string> GetAllTagsInItems()
        {
            List<Item> items = GetItemsFromInstance();
            List<string> r = new List<string>();
            foreach (Item i in items)
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
            List<Item> items = GetItemsFromInstance();
            List<string> r = new List<string>();
            foreach (Item i in items)
            {
                if (i.InGroup() && !r.Contains(i.group)) r.Add(i.group);
            }
            return new List<string>(r);
        }
        public bool Contain(Predicate<Item> match) => DataUtility.ListContainItem(match, GetItemsFromInstance());
        public Item GetItem(Predicate<Item> match) => DataUtility.GetItemInList(match, GetItemsFromInstance());
        public List<Item> GetItems(Predicate<Item> match) => DataUtility.GetItemsInList(match, GetItemsFromInstance());
        public bool AddItem(Item newItem)=> DataUtility.AddItemToList(newItem, GetItemsFromInstance());
        public Item AddItem(string newItemName) => DataUtility.AddItemToList(newItemName, GetItemsFromInstance());
        public int RemoveItems(Predicate<Item> match) => DataUtility.RemoveItemsInList(match, GetItemsFromInstance());

        [FoldoutGroup("Advance Edit", Order = -94)]
        
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
        [FoldoutGroup("Advance Edit/Edit Variable By Feature"), SerializeField, ListDrawerSettings(HideAddButton = true, ListElementLabelName = "itemName"), PropertyOrder(-2), PropertySpace(SpaceBefore = 5, SpaceAfter = 5)] List<Item> temp_editItemList;
        [FoldoutGroup("Advance Edit/Edit Variable By Feature"), EnumToggleButtons, SerializeField] FindItemMethod findItemMethod;
        [FoldoutGroup("Advance Edit/Edit Variable By Feature"), ShowIf("@findItemMethod.ToString() == \"group\""), ValueDropdown("@dic_groups.Keys"), SerializeField] string temp_selectGroup;
        [FoldoutGroup("Advance Edit/Edit Variable By Feature"), ShowIf("@findItemMethod.ToString() == \"tag\""), EnumToggleButtons, SerializeField] FindBool searchBool;
        [FoldoutGroup("Advance Edit/Edit Variable By Feature"), ShowIf("@findItemMethod.ToString() == \"tag\""), ValueDropdown("@OdinUtility.ValueDropDownListSelector(dic_tags.Keys,temp_selectTag)"), SerializeField] List<string> temp_selectTag;
        [FoldoutGroup("Advance Edit/Edit Variable By Feature"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void FindItems()
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
        [FoldoutGroup("Advance Edit/Edit Variable By Feature/Other Search Method"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void FindRepeatItem()
        {
            List<Item> items = GetItemsFromInstance();
            InitiateNullDatas();
            temp_editItemList = new List<Item>();
            List<string> names = new List<string>();
            foreach (Item i in items)
            {
                if (names.Contains(i.itemName)) temp_editItemList = temp_editItemList.Concat(GetItems(x => x.itemName == i.itemName)).ToList();
                else names.Add(i.itemName);
            }
        }
        [FoldoutGroup("Advance Edit/Edit Variable By Feature/Other Search Method"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.7f, 1, 0.7f)]
        void FindUnNamedItem()
        {
            InitiateNullDatas();
            temp_editItemList = GetItems(x => string.IsNullOrEmpty(x.itemName) || string.IsNullOrWhiteSpace(x.itemName));
        }
        [FoldoutGroup("Advance Edit/Edit Variable By Feature/Other Search Method"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void FindUnGroupedItem()
        {
            InitiateNullDatas();
            temp_editItemList = GetItems(x => !x.InGroup());
        }
        [FoldoutGroup("Advance Edit/Edit Variable By Feature/Other Search Method"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.7f, 1, 0.7f)]
        void FindUnTagedItem()
        {
            InitiateNullDatas();
            temp_editItemList = GetItems(x => x.tags == null || x.tags.Count == 0);
        }


        [FoldoutGroup("Advance Edit/Group Edit"), Button(ButtonHeight = 40, Style = ButtonStyle.Box), GUIColor(1f, 0.4f, 0.4f)]
        void RemoveGroup([ValueDropdown("@dic_groups.Keys")] string selectGroup, [LabelWidth(180)] bool removeContainedItem = true)
        {
            List<Item> items = GetItemsFromInstance();
            if (string.IsNullOrWhiteSpace(selectGroup)) return;
            if (removeContainedItem) RemoveItems(x => x.group == selectGroup);
            else
            {
                foreach (Item i in items)
                {
                    if (i.group == selectGroup) i.group = "";
                }
            }
        }
        [FoldoutGroup("Advance Edit/Group Edit"), Button(ButtonSizes.Large, Style = ButtonStyle.Box), GUIColor(0.4f, 1, 0.4f)]
        void RenameGroup([ValueDropdown("@dic_groups.Keys")] string selectGroup, string newName)
        {
            List<Item> getItemMatch = GetItems(x => x.InGroup(selectGroup));
            foreach (Item i in getItemMatch) i.group = newName;
        }

        [FoldoutGroup("Advance Edit/Tag Edit"), ValueDropdown("@OdinUtility.ValueDropDownListSelector(dic_tags.Keys,selectTag)"), SerializeField] List<string> selectTag;
        [FoldoutGroup("Advance Edit/Tag Edit"), LabelWidth(180), SerializeField] bool removeAttachedItem = false;
        [FoldoutGroup("Advance Edit/Tag Edit"), Button(ButtonHeight = 40), GUIColor(1f, 0.4f, 0.4f)]
        void RemoveTag()
        {
            List<Item> items = GetItemsFromInstance();
            if (removeAttachedItem) RemoveItems(x => x.HaveTags(selectTag));
            else
            {
                foreach (Item i in items)
                {
                    i.RemoveTags(selectTag);
                }
            }
        }
        [FoldoutGroup("Advance Edit/Tag Edit"), Button(ButtonSizes.Large, Style = ButtonStyle.Box), GUIColor(0.4f, 1, 0.4f)]
        void RenameTag([ValueDropdown("@dic_tags.Keys")] string selectTag, string newName)
        {
            List<Item> getMatchItems = GetItems(x => x.HaveTag(selectTag));
            foreach (Item i in getMatchItems)
            {
                i.RemoveTag(selectTag);
                i.AddTag(newName);
            }
        }


        /*[SerializeField,HideInInspector]
        private List<Item> temp_items;
        public void OnAfterDeserialize()
        {
            temp_items = new List<Item>(items);
        }

        public void OnBeforeSerialize()
        {
            items = temp_items;
        }*/
    }

    public class abc : MonoBehaviour, ISerializationCallbackReceiver
    {
        public void OnAfterDeserialize()
        {
            throw new NotImplementedException();
        }

        public void OnBeforeSerialize()
        {
            throw new NotImplementedException();
        }
    }
}

