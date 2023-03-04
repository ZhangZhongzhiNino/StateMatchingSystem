using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;



namespace Nino.NewStateMatching
{
    [InlineEditor]
    public abstract class OldDataController : DataScriptableObject 

    {
        [ReadOnly,LabelWidth(80),PropertyOrder(-101)]public string dataType;
        [FoldoutGroup("Hint",Order = -99),TextArea(minLines:5,maxLines:20),SerializeField] string hint;
        [FoldoutGroup("Note",Order =-98), TextArea(minLines: 5, maxLines: 20), SerializeField] string note;
        [FoldoutGroup("Data",Order=-97),PropertyOrder(1),PropertySpace(SpaceAfter = 20, SpaceBefore = 10),ListDrawerSettings(ListElementLabelName ="itemName")]public List<OldItem> items;


        [FoldoutGroup("Data Output Reference",Order = -96)] public Dictionary<string, OldItem> dic_items;
        [FoldoutGroup("Data Output Reference")] public Dictionary<string, List<OldItem>> dic_groups;
        [FoldoutGroup("Data Output Reference")] public Dictionary<string, List<OldItem>> dic_tags;
        [FoldoutGroup("Data Output Reference"),Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)] void UpdateDictionrary()
        {
            RemoveRedundantTagsInAllItems();
            dic_items = new Dictionary<string, OldItem>();
            dic_groups = new Dictionary<string, List<OldItem>>();
            dic_tags = new Dictionary<string, List<OldItem>>();
            foreach(OldItem item in items)
            {
                dic_items.Clear();
                dic_groups.Clear();
                dic_tags.Clear();
                if (!dic_items.Keys.Contains(item.itemName)) dic_items.Add(item.itemName, item);
                if (!string.IsNullOrWhiteSpace(item.group))
                {
                    if (!dic_groups.Keys.Contains(item.group)) dic_groups.Add(item.group, new List<OldItem>());
                    dic_groups[item.group].Add(item);
                }
                
                foreach(string tag in item.tags)
                {
                    if (!string.IsNullOrWhiteSpace(tag))
                    {
                        if (!dic_tags.Keys.Contains(tag)) dic_tags.Add(tag, new List<OldItem>());
                        dic_tags[tag].Add(item);
                    }
                    
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
                AssetUtility.SaveAsset(this, rootPath + "/" + dataType + "_Controller.asset");
            }
            else
            {
                AssetDatabase.SaveAssetIfDirty(this);
            }
            UnityEditor.EditorGUIUtility.PingObject(this);
            
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
            items = new List<OldItem>();
            dic_items = new Dictionary<string, OldItem>();
            dic_groups = new Dictionary<string, List<OldItem>>();
            dic_tags = new Dictionary<string, List<OldItem>>();
        }
        protected abstract string WriteHint();
        protected abstract string WriteDataType();
        protected override void RunOnEveryEnable()
        {
            foreach(OldItem i in items)
            {
                if (i.ResetWenEnabled) i.ResetData();
            }
        }
        protected abstract OldItem CreateNewItem();

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
        [FoldoutGroup("Advance Edit")]
        [FoldoutGroup("Advance Edit/ Edit By Feature"), SerializeField, ListDrawerSettings(HideAddButton = true, ListElementLabelName = "itemName"), PropertyOrder(-2), PropertySpace(SpaceBefore = 5, SpaceAfter = 5)] List<OldItem> temp_editItemList;
        [FoldoutGroup("Advance Edit/ Edit By Feature/Search By Tags or Groups"), EnumToggleButtons, SerializeField] FindItemMethod findItemMethod;
        [FoldoutGroup("Advance Edit/ Edit By Feature/Search By Tags or Groups"), ShowIf("@findItemMethod.ToString() == \"group\""), ValueDropdown("@dic_groups.Keys"), SerializeField] string temp_selectGroup;
        [FoldoutGroup("Advance Edit/ Edit By Feature/Search By Tags or Groups"), ShowIf("@findItemMethod.ToString() == \"tag\""), EnumToggleButtons, SerializeField] FindBool searchBool;
        [FoldoutGroup("Advance Edit/ Edit By Feature/Search By Tags or Groups"), ShowIf("@findItemMethod.ToString() == \"tag\""), ValueDropdown("@OdinUtility.ValueDropDownListSelector(tags,temp_selectTag)"), SerializeField] List<string> temp_selectTag;
        [FoldoutGroup("Advance Edit/ Edit By Feature/Search By Tags or Groups"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void FindItems()
        {
            InitiateNullDatas();
            temp_editItemList = new List<OldItem>();
            if (findItemMethod == FindItemMethod.group) temp_editItemList = items.FindAll(x => x.group == temp_selectGroup);
            else if (findItemMethod == FindItemMethod.tag && searchBool == FindBool.ContainAll) temp_editItemList = items.FindAll(x => x.HaveTags(temp_selectTag));
            else
            {
                foreach (string tag in temp_selectTag)
                {
                    temp_editItemList = temp_editItemList.Concat(items.FindAll(x => x.HaveTag(tag))).ToList();
                }
                temp_editItemList = temp_editItemList.Distinct().ToList();
            }
        }
        [FoldoutGroup("Advance Edit/ Edit By Feature/Other Search Method"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)] void FindRepeatItem()
        {
            InitiateNullDatas();
            temp_editItemList = new List<OldItem>();
            List<string> names = new List<string>();
            foreach (OldItem i in items)
            {
                if (names.Contains(i.itemName)) temp_editItemList = temp_editItemList.Concat(items.FindAll(x => x.itemName == i.itemName)).ToList();
                else names.Add(i.itemName);
            }
        }
        [FoldoutGroup("Advance Edit/ Edit By Feature/Other Search Method"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.7f, 1, 0.7f)] void FindUnNamedItem()
        {
            InitiateNullDatas();
            temp_editItemList = items.FindAll(x => string.IsNullOrEmpty(x.itemName) || string.IsNullOrWhiteSpace(x.itemName));
        }
        [FoldoutGroup("Advance Edit/ Edit By Feature/Other Search Method"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)] void FindUnGroupedItem()
        {
            InitiateNullDatas();
            temp_editItemList = items.FindAll(x => !x.InGroup());
        }
        [FoldoutGroup("Advance Edit/ Edit By Feature/Other Search Method"), Button(Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.7f, 1, 0.7f)] void FindUnTagedItem()
        {
            InitiateNullDatas();
            temp_editItemList = items.FindAll(x => x.tags == null || x.tags.Count == 0);
        }

        [FoldoutGroup("Data"), Button(ButtonSizes.Large), GUIColor(1f, 1f, 0.4f), PropertyOrder(-2)] void _RemoveRedundantTagsInAllItems()
        {
            RemoveRedundantTagsInAllItems();
            UpdateDictionrary();
        }
        public void RemoveRedundantTagsInAllItems()
        {
            foreach (OldItem i in items) i.RemoveRedundantTags();
        }
        [HorizontalGroup("Data/DataEditButton"), Button(ButtonSizes.Large), GUIColor(1f, 0.4f, 0.4f), PropertyOrder(-1)] void _RemoveRedundantDatas()
        {
            RemoveRedundantDatas();
            UpdateDictionrary();
        }
        void RemoveRedundantDatas()
        {
            items.RemoveAll(x => x == null || x == default(OldItem) || string.IsNullOrWhiteSpace(x.itemName));
            List<string> names = new List<string>();
            List<OldItem> toRemove = new List<OldItem>();
            foreach (OldItem i in items)
            {
                if (names.Contains(i.itemName)) toRemove.Add(i);
                else names.Add(i.itemName);
            }
            foreach (OldItem i in toRemove)
            {
                items.Remove(i);
            }
        }
        [HorizontalGroup("Data/DataEditButton"), Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)] void InitiateNullDatas()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == null) items[i] = CreateNewItem();
                if (items[i].value == null) items[i].value = items[i].CreateNewValue();
            }
        }


        [FoldoutGroup("Advance Edit/Groups"), Button(ButtonHeight = 40, Style = ButtonStyle.Box), GUIColor(1f, 0.4f, 0.4f)]
        void RemoveGroup([ValueDropdown("@dic_groups.Keys")] string selectGroup, [LabelWidth(180)] bool removeContainedItem = true)
        {
            if (string.IsNullOrWhiteSpace(selectGroup)) return;
            if (removeContainedItem) items.RemoveAll(x => x.group == selectGroup);
            else
            {
                foreach (OldItem i in items)
                {
                    if (i.group == selectGroup) i.group = "";
                }
            }
            UpdateDictionrary();
        }
        [FoldoutGroup("Advance Edit/Groups"), Button(ButtonSizes.Large, Style = ButtonStyle.Box), GUIColor(0.4f, 1, 0.4f)]
        void RenameGroup([ValueDropdown("@dic_groups.Keys")] string selectGroup, string newName)
        {
            List<OldItem> getItemMatch = items.FindAll(x => x.InGroup(selectGroup));
            foreach (OldItem i in getItemMatch) i.group = newName;
            UpdateDictionrary();
        }
        [FoldoutGroup("Advance Edit/Tagss"), ValueDropdown("@OdinUtility.ValueDropDownListSelector(dic_tags.Keys,selectTag)"), SerializeField] List<string> selectTag;
        [FoldoutGroup("Advance Edit/Tagss"), LabelWidth(180), SerializeField] bool removeAttachedItem = false;
        [FoldoutGroup("Advance Edit/Tagss"), Button(ButtonHeight = 40), GUIColor(1f, 0.4f, 0.4f)]
        void RemoveTag()
        {
            if (removeAttachedItem) items.RemoveAll(x => x.HaveTags(selectTag));
            else
            {
                foreach (OldItem i in items)
                {
                    i.RemoveTags(selectTag);
                }
            }
            UpdateDictionrary();
        }
        [FoldoutGroup("Advance Edit/Tagss"), Button(ButtonSizes.Large, Style = ButtonStyle.Box), GUIColor(0.4f, 1, 0.4f)]
        void RenameTag([ValueDropdown("@dic_tags.Keys")] string selectTag, string newName)
        {
            List<OldItem> getMatchItems = items.FindAll(x => x.HaveTag(selectTag));
            foreach (OldItem i in getMatchItems)
            {
                i.RemoveTag(selectTag);
                i.AddTag(newName);
            }
            UpdateDictionrary();
        }


        public List<string> GetAllItemNames()
        {
            List<string> r = new List<string>();
            foreach (OldItem i in items)
            {
                if (!r.Contains(i.itemName)) r.Add(i.itemName);
            }
            return new List<string>(r);
        }
        public List<string> GetAllTagsInItems()
        {
            List<string> r = new List<string>();
            foreach (OldItem i in items)
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
            foreach (OldItem i in items)
            {
                if (i.InGroup() && !r.Contains(i.group)) r.Add(i.group);
            }
            return new List<string>(r);
        }
        public bool Contain(Predicate<OldItem> match) => DataUtility.ListContainItem(match, items);
        public bool AddItem(OldItem newItem) => DataUtility.OldAddItemToList(newItem, items);
        public OldItem AddItem(string newItemName)
        {
            if (items.Find(x => x.itemName == newItemName) != null) return items.Find(x => x.itemName == newItemName);
            OldItem newItem = CreateNewItem();
            newItem.itemName = newItemName;
            AddItem(newItem);
            return newItem;
        }

    }
}

