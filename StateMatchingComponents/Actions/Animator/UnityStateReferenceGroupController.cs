using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
using UnityEditor.Animations;
using System;
using Sirenix.OdinInspector;
namespace StateMatching.Action
{
    public class UnityStateReferenceGroupController : GroupController<UnityStateReference, UnityStateReferenceValue>
    {
        public override Type getGroupType()
        {
            return typeof(UnityStateReferenceGroup);
        }
    }
}
