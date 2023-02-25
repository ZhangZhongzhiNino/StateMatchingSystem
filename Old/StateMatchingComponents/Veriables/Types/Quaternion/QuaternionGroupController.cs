using System;
using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class QuaternionGroupController : VariableGroupContoller<Quaternion>
    {
        public override Type getGroupType()
        {
            return typeof(QuaternionGroup);
        }

        public override Type getItemType()
        {
            return typeof(QuaternionItem);
        }
    }
}

