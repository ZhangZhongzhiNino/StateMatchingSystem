using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
using System;

namespace StateMatching.Data
{
    public class PoseDataGroupController : GroupController<PoseDataItem>
    {
        
        public override Type getGroupType()
        {
            return typeof(PoseDataGroup);
        }

        public override Type getItemType()
        {
            return typeof(PoseDataItem);
        }
    }
}