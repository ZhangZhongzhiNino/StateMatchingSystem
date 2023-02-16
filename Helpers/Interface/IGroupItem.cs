using UnityEngine;
namespace StateMatching.Helper
{
    public interface IGroupItem<T,V> where T : MonoBehaviour 
    {
        string itemName { get; set; }
        public void AssignItem(T item);
        V value { get; set; }
    }
}
