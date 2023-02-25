using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
using UnityEditor.Animations;
using System;
using Sirenix.OdinInspector;
namespace Nino.StateMatching.Action
{
    public class UnityStateReferenceGroupController : GroupController<UnityStateReferenceValue>
    {
        public override Type getGroupType()
        {
            return typeof(UnityStateReferenceGroup);
        }

        public override Type getItemType()
        {
            return typeof(UnityStateReferenceItem);
        }
    }
}
