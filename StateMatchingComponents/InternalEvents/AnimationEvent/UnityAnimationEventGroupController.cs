using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
using System;

namespace StateMatching.InternalEvent
{
    public class UnityAnimationEventGroupController : GroupController<UnityAnimationEventItem>
    {
        public override Type getGroupType()
        {
            return typeof(UnityAnimationEventGroup);
        }

        public override Type getItemType()
        {
            return typeof(UnityAnimationEventItem);
        }
    }
}

