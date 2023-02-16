using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMatching.Helper
{
    public class Group<T,V> : MonoBehaviour where T : MonoBehaviour, IGroupItem<T,V> where V: class
    {
        public List<T> items;
        public string groupName;
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
            items.Add(item);
        }
    }
}
