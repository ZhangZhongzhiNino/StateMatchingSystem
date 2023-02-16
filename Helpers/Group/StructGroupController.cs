using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;


namespace StateMatching.Helper
{

    public abstract class StructGroupController<T,V> : MonoBehaviour where T: MonoBehaviour,IGroupItem<T,V> where V: struct
    {
        [FoldoutGroup("Reference")][ReadOnly] public List<T> items;
        [FoldoutGroup("Reference")][ReadOnly] public List<StructGroup<T,V>> groups;
        [FoldoutGroup("Reference")][ReadOnly] public GameObject itemsHolder;
        public abstract System.Type getGroupType();

        public StructGroup<T, V> addNewGroupToGameObject()
        {
            StructGroup<T,V> newGroup = gameObject.AddComponent(getGroupType()) as StructGroup<T,V>;
            return newGroup;
        }
        #region Item
        public void UpdateItemsList()
        {
            if(itemsHolder) items = itemsHolder.GetComponents<T>().ToList();
        }
        public void RemoveItem(string itemName)
        {
            T toRemoveItem = GetItem(itemName);
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
            foreach(T item in items)
            {
                Helpers.RemoveComponent(item);
            }
            ClearNullInGroups();
            
        }
        public T AddItem(string itemName,T item)
        {
            T tryGetItem = GetItem(itemName);
            if (tryGetItem)
            {
                tryGetItem.AssignItem(item);
            }
            else
            {
                tryGetItem = itemsHolder.AddComponent<T>();
                tryGetItem.AssignItem(item);
                tryGetItem.itemName = itemName;
            }
            
            UpdateItemsList();
            return tryGetItem;
        }
        public T AddItem(string itemName, T item, string groupName)
        {
            //if (T == null || !T.IsAssignableFrom(typeof(_T))) return;
            T newItem = AddItem(itemName,item);
            StructGroup<T,V> findGroup = AddNewGroup(groupName);
            findGroup.items.Add(newItem);
            UpdateItemsList();
            return newItem;

        }
        #endregion
        #region Groups
        public void UpdateGroupsList()
        {
            groups = this.GetComponents<StructGroup<T,V>>().ToList();
        }
        public StructGroup<T,V> AddNewGroup(string groupName)
        {
            StructGroup<T,V> newGroup = GetGroup(groupName);
            if (newGroup) return newGroup;
            newGroup = addNewGroupToGameObject();
            newGroup.Initiate(groupName, this);
            UpdateGroupsList();
            return newGroup;
        }
        public bool RemoveGroup(string groupName)
        {
            StructGroup<T,V> getGroup = GetGroup(groupName);
            if (!getGroup) return false;
            Helpers.RemoveComponent(getGroup);
            UpdateGroupsList();
            return true;
        }
        public bool AddItemToGroup(string groupName, string itemName)
        {
            T tryGetItem = GetItem(itemName);
            if (!tryGetItem) return false;
            StructGroup<T,V> newGroup = AddNewGroup(groupName);
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
            StructGroup<T, V> getGroup = GetGroup(groupName);
            return (getGroup.items == null || getGroup.items.Count == 0);
        }
        public bool GroupIsEmpty(int groupIndex)
        {
            StructGroup<T, V> getGroup = GetGroup(groupIndex);
            return (getGroup.items == null || getGroup.items.Count == 0);
        }
        #endregion
        #region Get Set functions
        public T GetItem(string groupName, string itemName)
        {
            //if (T == null || !T.IsAssignableFrom(typeof(_T))) return null;
            StructGroup<T,V> getGroup = GetGroup(groupName);
            if (!getGroup) return null;
            List<T> _items = getGroup.items.ToList();
            if (_items == null) return null;
            return _items.Find(item => item.itemName == itemName);
        }
        public T GetItem( string itemName)
        {
            return items.Find(item => item.itemName == itemName);
        }
        public List<T> GetItems() { return new List<T>(items); }
        public List<string> GetItemNameList(string groupName)
        {
            if (groupName == null || string.IsNullOrEmpty(groupName)) return null;
            StructGroup<T, V> getGroup = GetGroup(groupName);
            if (!getGroup || getGroup.items==null || getGroup.items.Count ==0) return null;
            List<string> rList = new List<string>();
            foreach(T item in getGroup.items)
            {
                rList.Add(item.itemName);
            }
            return rList;
        }
        public List<string> GetItemNameList()
        {
            List<T> itemList = items;
            List<string> rList = new List<string>();
            foreach (T item in itemList)
            {
                rList.Add(item.itemName);
            }
            return rList;
        }
        public StructGroup<T,V> GetGroup(string groupName)
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
        public StructGroup<T, V> GetGroup(int groupIndex)
        {
            if (groups != null && groupIndex < groups.Count)
            {
                return groups[groupIndex];
            }
            return null;
        }
        public List<StructGroup<T,V>> GetGroups() { return new List<StructGroup<T,V>>(groups); }
        public List<string> GetGroupNameList()
        {
            if (groups == null || groups.Count == 0) return null;
            List<string> rList = new List<string>();
            foreach(StructGroup<T,V> group in groups)
            {
                rList.Add(group.groupName);
            }
            return rList;
        }
        public bool SetItemValue(string groupName, string itemName, V newValue)
        {
            T getItem = GetItem(groupName, itemName);
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
            foreach(StructGroup<T,V> group in groups)
            {
                group.items.RemoveAll(item => item == null);
            }
        }
        public void RemoveAllInItems(Predicate<T> match)
        {
            Helpers.RemoveAllComponentInGameObject<T>(itemsHolder,match);
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
            items = new List<T>();
            groups = new List<StructGroup<T,V>>();
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
                foreach (StructGroup<T,V> group in groups)
                {
                    newList.Add(group.groupName);
                }
                if (newList.Count == 0) return null;
                return newList;
            }
        }
        StructGroup<T,V> currentSelectGroup
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
                foreach (T item in currentSelectGroup.items)
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
                foreach(T item in items)
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
                foreach(T item in currentSelectGroup.items)
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
                StructGroup<T,V> getGroup = GetGroup(selectGroup);
                if (!getGroup) return null;
                List<string> newList = new List<string>();
                foreach(T item in getGroup.items)
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
            StructGroup<T,V> newGroup = AddNewGroup(newGroupName);
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
