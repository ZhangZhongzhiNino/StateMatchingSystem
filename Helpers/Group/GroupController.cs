using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;


namespace StateMatching.Helper
{

    public abstract class GroupController<V> : MonoBehaviour
    {
        [FoldoutGroup("Reference")][ReadOnly] public List<Item<V>> items;
        [FoldoutGroup("Reference")][ReadOnly] public List<Group<V>> groups;
        [FoldoutGroup("Reference")][ReadOnly] public GameObject itemsHolder;
        public abstract System.Type getGroupType();
        public abstract System.Type getItemType();
        //Need To Overide
        public Group<V> addNewGroupToGameObject()
        {
            Group <V> newGroup = gameObject.AddComponent(getGroupType()) as Group<V>;
            return newGroup;
        }
        #region Item
        public void UpdateItemsList()
        {
            if(itemsHolder) items = itemsHolder.GetComponents<Item<V>>().ToList();
        }
        public void RemoveItem(string itemName)
        {
            Item<V> toRemoveItem = GetItem(itemName);
            if (!toRemoveItem) return;
            Helpers.RemoveComponent(toRemoveItem);
            UpdateItemsList();
            ClearNullInGroups();
            ClearNullInItems();
            
        }
        public void RemoveItem(int index)
        {
            if (index >= items.Count) return;
            Helpers.RemoveComponent(items[index]);
            ClearNullInGroups();
            ClearNullInItems();
        }
        public void RemoveAllItem()
        {
            foreach(Item<V> item in items)
            {
                Helpers.RemoveComponent(item);
            }
            ClearNullInGroups();
            
        }
        public Item<V> AddItem(string itemName, Item<V> item)
        {
            Item<V> tryGetItem = GetItem(itemName);
            if (tryGetItem)
            {
                tryGetItem.AssignItem(item);
            }
            else
            {
                tryGetItem = itemsHolder.AddComponent(getItemType()) as Item<V>;
                tryGetItem.AssignItem(item);
                tryGetItem.itemName = itemName;
            }
            
            UpdateItemsList();
            return tryGetItem;
        }
        public Item<V> AddItem(string itemName, Item<V> item, string groupName)
        {
            //if (T == null || !T.IsAssignableFrom(typeof(_T))) return;
            Item<V> newItem = AddItem(itemName,item);
            Group<V> findGroup = AddNewGroup(groupName);
            AddItemToGroup(groupName, itemName);
            UpdateItemsList();
            return newItem;

        }
        #endregion
        #region Groups
        public void UpdateGroupsList()
        {
            groups = this.GetComponents<Group<V>>().ToList();
        }
        public Group<V> AddNewGroup(string groupName)
        {
            Group<V> newGroup = GetGroup(groupName);
            if (newGroup) return newGroup;
            newGroup = addNewGroupToGameObject();
            newGroup.Initiate(groupName, this);
            UpdateGroupsList();
            return newGroup;
        }
        public bool RemoveGroup(string groupName)
        {
            Group<V> getGroup = GetGroup(groupName);
            if (!getGroup) return false;
            Helpers.RemoveComponent(getGroup);
            UpdateGroupsList();
            return true;
        }
        public bool AddItemToGroup(string groupName, string itemName)
        {
            Item<V> tryGetItem = GetItem(itemName);
            if (!tryGetItem) return false;
            Group<V> newGroup = AddNewGroup(groupName);
            newGroup.AddItem(tryGetItem);
            return true;
        }
        public bool RemoveItemFromGroup(string groupName,string itemName)
        {
            if (!GetItem(groupName, itemName)) return false;
            GetGroup(groupName)?.items.Remove(GetItem(groupName, itemName));
            return true;
        }
        public void RemoveAllGroups()
        {
            for(int i = 0; i < groups.Count; i++)
            {
                Helpers.RemoveComponent(groups[i]);
            }
            UpdateGroupsList();
        }
        public bool GroupIsEmpty(string groupName)
        {
            Group<V> getGroup = GetGroup(groupName);
            return (getGroup.items == null || getGroup.items.Count == 0);
        }
        public bool GroupIsEmpty(int groupIndex)
        {
            Group<V> getGroup = GetGroup(groupIndex);
            return (getGroup.items == null || getGroup.items.Count == 0);
        }
        #endregion
        #region Get Set functions
        public Item<V> GetItem(string groupName, string itemName)
        {
            //if (T == null || !T.IsAssignableFrom(typeof(_T))) return null;
            Group<V> getGroup = GetGroup(groupName);
            if (!getGroup) return null;
            List<Item<V>> _items = getGroup.items.ToList();
            if (_items == null) return null;
            return _items.Find(item => item.itemName == itemName);
        }
        public Item<V> GetItem( string itemName)
        {
            return items.Find(item => item.itemName == itemName);
        }
        public List<Item<V>> GetItems() { return new List<Item<V>>(items); }
        public List<string> GetItemNameList(string groupName)
        {
            if (groupName == null || string.IsNullOrEmpty(groupName)) return null;
            Group<V> getGroup = GetGroup(groupName);
            if (!getGroup || getGroup.items==null || getGroup.items.Count ==0) return null;
            List<string> rList = new List<string>();
            foreach(Item<V> item in getGroup.items)
            {
                rList.Add(item.itemName);
            }
            return rList;
        }
        public List<string> GetItemNameList()
        {
            List<Item<V>> itemList = items;
            List<string> rList = new List<string>();
            foreach (Item<V> item in itemList)
            {
                rList.Add(item.itemName);
            }
            return rList;
        }
        public Group<V> GetGroup(string groupName)
        {
            if (groups != null)
            {
                for (int i = 0; i < groups.Count; i++)
                {
                    if (groups[i].groupName == groupName)
                    {
                        return groups[i];
                    }
                }
            }
            return null;
        }
        public Group<V> GetGroup(int groupIndex)
        {
            if (groups != null && groupIndex < groups.Count)
            {
                return groups[groupIndex];
            }
            return null;
        }
        public List<Group<V>> GetGroups() { return new List<Group<V>>(groups); }
        public List<string> GetGroupNameList()
        {
            if (groups == null || groups.Count == 0) return null;
            List<string> rList = new List<string>();
            foreach(Group<V> group in groups)
            {
                rList.Add(group.groupName);
            }
            return rList;
        }
        public bool SetItemValue(string groupName, string itemName, V newValue)
        {
            Item<V> getItem = GetItem(groupName, itemName);
            if (!getItem) return false;
            getItem.value = newValue;
            return true;
        }
        
        #endregion
        #region Helpers
        public void ClearNullInItems()
        {
            items.RemoveAll(item => item == null);
            UpdateItemsList();
        }
        public void ClearNullInGroups()
        {
            foreach(Group<V> group in groups)
            {
                group.items.RemoveAll(item => item == null);
            }
        }
        public void RemoveAllInItems(Predicate<Item<V>> match)
        {
            Helpers.RemoveAllComponentInGameObject<Item<V>>(itemsHolder,match);
            ClearNullInItems();
            ClearNullInGroups();
            UpdateItemsList();
        }
        #endregion
        #region SetUp Destroy
        public void Initialize( GameObject _itemsHolder)
        {
            if(!_itemsHolder) _itemsHolder = new GameObject();
            itemsHolder = _itemsHolder;
            items = new List<Item<V>>();
            groups = new List<Group<V>>();
        }
        public void PreDestroy()
        {
            Helpers.RemoveGameObject(itemsHolder);
            Helpers.RemoveGameObject(this.gameObject);
        }
        #endregion

        #region Odin
        #region Values
        List<string> groupNames
        {
            get
            {
                List<string> newList = new List<string>();
                foreach (Group<V> group in groups)
                {
                    newList.Add(group.groupName);
                }
                if (newList.Count == 0) return null;
                return newList;
            }
        }
        Group<V> currentSelectGroup
        {
            get
            {
                return GetGroup(selectGroup);
            }
        }
        List<string> namesOfItemInGroup
        {
            get
            {
                if (currentSelectGroup == null) return null;
                List<string> newList = new List<string>();
                foreach (Item<V> item in currentSelectGroup.items)
                {
                    newList.Add(item.itemName);
                }
                return newList;
            }
        }
        List<string> allItemNames
        {
            get
            {
                if (items ==null||items.Count == 0) return null;
                List<string> newList = new List<string>();
                foreach(Item<V> item in items)
                {
                    newList.Add(item.itemName);
                }
                return newList;
                
            }
        }
        List<string> allItemInSelectedGroup 
        {
            get
            {
                if (!currentSelectGroup) return null;
                List<string> newList = new List<string>();
                foreach(Item<V> item in currentSelectGroup.items)
                {
                    newList.Add(item.itemName);
                }
                return newList;
            }
        }
        List<string> allItemOutSelectedGroup
        {
            get
            {
                if (allItemInSelectedGroup==null) return null;
                if (items.Count == 0) return null;
                List<string> newList = new List<string>(allItemNames);
                foreach (string n in allItemInSelectedGroup)
                {
                    newList.RemoveAll(item => item == n);
                }
                return newList;
            }
        }
        #endregion
        #region ViewGroups
        [FoldoutGroup("View Groups"), ShowInInspector, ListDrawerSettings(Expanded = true)]
        List<string> contains
        {
            get
            {
                Group<V> getGroup = GetGroup(selectGroup);
                if (!getGroup) return null;
                List<string> newList = new List<string>();
                foreach(Item<V> item in getGroup.items)
                {
                    newList.Add(item.itemName);
                }
                return newList;
            }
        }
        
        #endregion
        #region Create & Remove Group
        [FoldoutGroup("Create Groups"), TitleGroup("Create Groups/Create Group", order: -999),SerializeField]
        string newGroupName;
        [TitleGroup("Create Groups/Create Group")]
        [Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)]
        void CreatGroup()
        {
            Group<V> newGroup = AddNewGroup(newGroupName);
            if (newGroup) Debug.Log("Group \"" + newGroupName + "\" Created");
            else Debug.Log("Group \"" + newGroupName + "\" already exist");
        }

        [TitleGroup("Create Groups/Remove Group", order: -998)]
        [ValueDropdown("@selectList(groupNames,selectGroupToRemove)"),SerializeField]
        [ListDrawerSettings(Expanded = true)] 
        List<string> selectGroupToRemove;
        [TitleGroup("Create Groups/Remove Group")]
        [Button(ButtonSizes.Large), GUIColor(1, 0.4f, 0.4f)]
        void _RemoveGroup()
        {
            foreach(string name in selectGroupToRemove)
            {
                RemoveGroup(name);
            }
            selectGroupToRemove.Clear();
        }
        #endregion
        #region Add Item To Group
        [FoldoutGroup("View Groups")]
        [FoldoutGroup("Edit groups")]
        [TitleGroup("Edit groups/Add item to Group", order: -998)]
        [TitleGroup("Edit groups/Remove item From Group", order: -997)]
        [ValueDropdown("groupNames"),SerializeField]
        string selectGroup;
        [TitleGroup("Edit groups/Add item to Group")]
        [SerializeField, ValueDropdown("@selectList(allItemOutSelectedGroup,selectItemsToAdd)")]
        [ListDrawerSettings(Expanded = true)]
        List<string> selectItemsToAdd;
        [TitleGroup("Edit groups/Add item to Group"), Button(buttonSize:50),GUIColor(0.4f,1,0.4f)]
        void _AddItemToGroup()
        {
            foreach(string name in selectItemsToAdd)
            {
                AddItemToGroup(selectGroup, name);
            }
            selectItemsToAdd.Clear();
            
        }
        #endregion
        #region Remove Item From Group
        [TitleGroup("Edit groups/Remove item From Group")]
        [ValueDropdown("@selectList(allItemInSelectedGroup,selectItemsToRemove)"),SerializeField]
        [ListDrawerSettings(Expanded = true)]
        List<string> selectItemsToRemove;
        [TitleGroup("Edit groups/Remove item From Group"), Button(buttonSize: 50), GUIColor(1f,0.4f, 0.4f)]
        void _RemoveItemFromGroup()
        {
            foreach(string name in selectItemsToRemove)
            {
                RemoveItemFromGroup(selectGroup, name);
            }
            selectItemsToRemove.Clear();
        }
        #endregion


        #region Custom Odin
        public IEnumerable<ValueDropdownItem<string>> selectList(List<string> list, List<string> selectList)
        {
            if (list == null) return new List<ValueDropdownItem<string>>();
            var items = new List<ValueDropdownItem<string>>();
            for (int i = 0; i < list.Count; i++)
            {
                if (selectList.Contains(list[i])) continue;
                items.Add(new ValueDropdownItem<string>(list[i], list[i]));
            }
            return items;
        }

        #endregion
        #endregion

        private void OnDrawGizmos()
        {
            ClearNullInItems();
            ClearNullInGroups();
        }


    }


}
