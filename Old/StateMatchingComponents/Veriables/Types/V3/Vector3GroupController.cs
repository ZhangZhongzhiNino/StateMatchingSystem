using System;
using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class Vector3GroupController : VariableGroupContoller< Vector3>
    {
        public override Type getGroupType()
        {
            return typeof(Vector3Group);
        }

        public override Type getItemType()
        {
            return typeof(Vector3Item);
        }
    }
}

