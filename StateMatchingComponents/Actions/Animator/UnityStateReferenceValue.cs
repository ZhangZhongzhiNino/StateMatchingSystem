using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
namespace StateMatching.Action 
{
    public class UnityStateReferenceValue 
    {
        public UnityEditor.Animations.AnimatorState state;
        public int layer;
        public UnityStateReferenceValue(int _layer, UnityEditor.Animations.AnimatorState _state)
        {
            state = _state;
            layer = _layer;
        }

    }
}


