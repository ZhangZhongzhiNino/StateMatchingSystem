using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using Nino.StateMatching.Helper;
using UnityEditor.Animations;

namespace Nino.StateMatching.Action
{
    public class UnityStateReferenceItem :Item<UnityStateReferenceValue>
    {
        public UnityStateReferenceValue _value;
        public override UnityStateReferenceValue value 
        { 
            get { return _value; }

            set { _value = value; }
        
        }
        public UnityStateReferenceItem(string _stateName, int layer, UnityEditor.Animations.AnimatorState _state)
        {
            itemName = _stateName;
            _value = new UnityStateReferenceValue(layer,_state);
        }

    
        


    }
}

