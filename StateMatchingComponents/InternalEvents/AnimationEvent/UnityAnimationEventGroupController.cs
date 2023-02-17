using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
using System;

namespace StateMatching.InternalEvent
{
    public class UnityAnimationEventGroupController : GroupController<UnityAnimationEvent, UnityAnimationEvent>
    {
        public override Type getGroupType()
        {
            return typeof(UnityAnimationEventGroup);
        }
    }
}

