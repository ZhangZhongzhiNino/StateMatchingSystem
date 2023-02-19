using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
using System;

namespace Nino.StateMatching.Data
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