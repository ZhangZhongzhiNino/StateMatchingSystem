using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace StateMatching.Helper
{
    public class Group<T,V> : MonoBehaviour where T : MonoBehaviour, IGroupItem<T,V> where V: class
    {
        [ListDrawerSettings(ListElementLabelName = "itemName")]
        public string groupName;
        public List<T> items;
        [HideInInspector]
        public GroupController<T,V> controller;
        public void Initiate(string _groupName, List<T> _items, GroupController<T,V> _controller)
        {
            controller = _controller;
            items = _items;
            groupName = _groupName;
        }
        public void Initiate(string _groupName, T _item,GroupController<T,V> _controller)
        {
            controller = _controller;
            items = new List<T>();
            items.Add(_item);
            groupName = _groupName;
        }
        public void Initiate(string _groupName, GroupController<T,V> _controller)
        {
            controller = _controller;
            items = new List<T>();
            groupName = _groupName;
        }
        public void AddItem(T item)
        {
            if (items.Contains(item)) return;
            items.Add(item);
        }
    }
}
