using System;
using UnityEngine;

namespace StateMatching.Variable
{
    public class QuaternionGroupController : VariableGroupContoller<QuaternionItem, Quaternion>
    {
        public override Type getGroupType()
        {
            return typeof(QuaternionGroup);
        }
    }
}

