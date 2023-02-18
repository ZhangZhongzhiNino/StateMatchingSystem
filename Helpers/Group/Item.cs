using UnityEngine;
namespace StateMatching.Helper
{
    public abstract class Item<V> : MonoBehaviour
    {
        public string itemName;
        public abstract V value { get; set; }

        public virtual void AssignItem(Item<V> item)
        {
            this.itemName = item.itemName;
            if(typeof(V) != this.GetType())this.value = item.value;
        }
    }
}
