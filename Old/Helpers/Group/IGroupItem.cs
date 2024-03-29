﻿using UnityEngine;
namespace Nino.StateMatching.Helper
{
    public interface IGroupItem<T,V> where T : MonoBehaviour 
    {
        public void AssignItem(T item);
    }
}
