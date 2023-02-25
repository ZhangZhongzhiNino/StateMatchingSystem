using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Nino.NewStateMatching
{
    [InlineEditor]
    public abstract class Collection<Item> : DataScriptableObject where Item: NewStateMatching.Item
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
}

