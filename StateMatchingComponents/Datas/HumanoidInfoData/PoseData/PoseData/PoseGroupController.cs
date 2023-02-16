using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
using System;

namespace StateMatching.Data
{
    public class PoseGroupController : GroupController<PoseData, PoseData>
    {
        
        public override Type getGroupType()
        {
            return typeof(PoseGroup);
        }
    }
}