using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using StateMatching.Helper;
using UnityEditor.Animations;

namespace StateMatching.Action
{
    public class UnityStateReference : MonoBehaviour,IGroupItem<UnityStateReference, UnityStateReferenceValue>
    {
        public string stateName;
        [ShowInInspector] UnityStateReferenceValue _value;
        public UnityStateReference(string _stateName, int layer, UnityEditor.Animations.AnimatorState _state)
        {
            stateName = _stateName;
            _value = new UnityStateReferenceValue(layer,_state);
        }

        #region IGroupItem
        public string itemName { get { return stateName; } set { stateName = value; } }

        public UnityStateReferenceValue value { get { return _value; } set { _value = value; } }

        public void AssignItem(UnityStateReference item)
        {
            itemName = item.itemName;
            _value = new UnityStateReferenceValue(item.value.layer,item.value.state);
            
        }
        #endregion
    }
}

