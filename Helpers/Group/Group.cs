using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace StateMatching.Helper
{
    public class Group<V> : MonoBehaviour
    {
        [ListDrawerSettings(ListElementLabelName = "itemName")]
        public string groupName;
        public List<Item<V>> items;
        [HideInInspector]
        public GroupController<V> controller;
        public void Initiate(string _groupName, List<Item<V>> _items, GroupController<V> _controller)
        {
            controller = _controller;
            items = _items;
            groupName = _groupName;
        }
        public void Initiate(string _groupName, Item<V> _item,GroupController<V> _controller)
        {
            controller = _controller;
            items = new List<Item<V>>();
            items.Add(_item);
            groupName = _groupName;
        }
        public void Initiate(string _groupName, GroupController<V> _controller)
        {
            controller = _controller;
            items = new List<Item<V>>();
            groupName = _groupName;
        }
        public void AddItem(Item<V> item)
        {
            if (items.Contains(item)) return;
            items.Add(item);
        }
    }
}
