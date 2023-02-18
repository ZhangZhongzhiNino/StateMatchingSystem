using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace StateMatching.Helper
{
    public class VeriableGroupPreview<V> : MonoBehaviour
    {
        [PropertyOrder(order:-999999)]public GroupController<V> groupController;
        
        #region Odin
        #region Values
        public List<string> groupNames
        {
            get
            {
                List<string> newList = new List<string>();
                foreach (Group<V> group in groupController.groups)
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
                return groupController.GetGroup(selectGroup);
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
                if (groupController.items == null || groupController.items.Count == 0) return null;
                List<string> newList = new List<string>();
                foreach (Item<V> item in groupController.items)
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
                foreach (Item<V> item in currentSelectGroup.items)
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
                if (allItemInSelectedGroup == null) return null;
                if (groupController.items.Count == 0) return null;
                List<string> newList = new List<string>(allItemNames);
                foreach (string n in allItemInSelectedGroup)
                {
                    newList.RemoveAll(item => item == n);
                }
                return newList;
            }
        }
        #endregion
        #region View Group

        List<string> groupNameList { get { return groupController?.GetGroupNameList(); } }

        [FoldoutGroup("View Group", order: -10)]
        [FoldoutGroup("Edit groups", order: -8)]
        [TitleGroup("Edit groups/Add item to Group", order: -998)]
        [TitleGroup("Edit groups/Remove item From Group", order: -997)]
        [ValueDropdown("groupNameList"), SerializeField]
        string selectGroup;
        Group<V> _selectGroup { get { return groupController.GetGroup(selectGroup) as Group<V>; } }
        [ShowIfGroup("View Group/List", Condition = "@ _selectGroup != null")]
        [ListDrawerSettings(Expanded = true), ShowInInspector, ReadOnly]
        List<string> itemNameList
        {
            get
            {
                List<string> newList = new List<string>();
                newList = groupController.GetItemNameList(selectGroup);
                if (newList == null) return null;
                for (int i = 0; i < newList.Count; i++)
                {
                    newList[i] += ": " + groupController.items[i].value.ToString();
                }
                return newList;
            }
            set { }
        }
        public void Initiate(GroupController<V> groupController)
        {
            this.groupController = groupController;
        }

        [FoldoutGroup("Items", order: -999), ShowInInspector]
        public List<string> AllItems
        {
            get
            {
                List<string> newList = new List<string>();
                newList = groupController.GetItemNameList();
                if (newList == null) return null;
                List<string> rList = new List<string>();
                for (int i = 0; i < newList.Count; i++)
                {
                    rList.Add(i.ToString() + ": " + newList[i] + " ---- " + groupController.items[i].value.ToString());
                    
                }
                return rList;
            }
        }
        #endregion
        #region Create & Remove Group
        [FoldoutGroup("Create Groups",order: -9), TitleGroup("Create Groups/Create Group", order: -999), SerializeField]
        string newGroupName;
        [TitleGroup("Create Groups/Create Group")]
        [Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)]
        void CreatGroup()
        {
            Group< V> newGroup = groupController.AddNewGroup(newGroupName);
            if (newGroup) Debug.Log("Group \"" + newGroupName + "\" Created");
            else Debug.Log("Group \"" + newGroupName + "\" already exist");
        }

        [TitleGroup("Create Groups/Remove Group", order: -998)]
        [ValueDropdown("@selectList(groupNames,selectGroupToRemove)"), SerializeField]
        [ListDrawerSettings(Expanded = true)]
        List<string> selectGroupToRemove;
        [TitleGroup("Create Groups/Remove Group")]
        [Button(ButtonSizes.Large), GUIColor(1, 0.4f, 0.4f)]
        void _RemoveGroup()
        {
            foreach (string name in selectGroupToRemove)
            {
                groupController.RemoveGroup(name);
            }
            selectGroupToRemove.Clear();
        }
        #endregion
        #region Add Item To Group
        
        [TitleGroup("Edit groups/Add item to Group")]
        [SerializeField, ValueDropdown("@selectList(allItemOutSelectedGroup,selectItemsToAdd)")]
        [ListDrawerSettings(Expanded = true)]
        List<string> selectItemsToAdd;
        [TitleGroup("Edit groups/Add item to Group"), Button(buttonSize: 50), GUIColor(0.4f, 1, 0.4f)]
        void _AddItemToGroup()
        {
            foreach (string name in selectItemsToAdd)
            {
                groupController.AddItemToGroup(selectGroup, name);
            }
            selectItemsToAdd.Clear();

        }
        #endregion
        #region Remove Item From Group
        [TitleGroup("Edit groups/Remove item From Group")]
        [ValueDropdown("@selectList(allItemInSelectedGroup,selectItemsToRemove)"), SerializeField]
        [ListDrawerSettings(Expanded = true)]
        List<string> selectItemsToRemove;
        [TitleGroup("Edit groups/Remove item From Group"), Button(buttonSize: 50), GUIColor(1f, 0.4f, 0.4f)]
        void _RemoveItemFromGroup()
        {
            foreach (string name in selectItemsToRemove)
            {
                groupController.RemoveItemFromGroup(selectGroup, name);
            }
            selectItemsToRemove.Clear();
        }
        #endregion
        #region Remove Item

        [FoldoutGroup("Items")]
        [Button]
        [GUIColor(1, 0.4f, 0.4f)]
        private void RemoveAllItems()
        {
            groupController.RemoveAllItem();
        }


        [FoldoutGroup("Items")]
        [Button(ButtonStyle.Box)]
        [GUIColor(1, 0.4f, 0.4f)]
        private void RemoveItemByName(string name)
        {
            groupController.RemoveItem(name);
        }


        [FoldoutGroup("Items")]
        [Button(ButtonStyle.Box)]
        [GUIColor(1, 0.4f, 0.4f)]
        private void RemoveItemByID(int index)
        {
            groupController.RemoveItem(index);
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
    }
}

