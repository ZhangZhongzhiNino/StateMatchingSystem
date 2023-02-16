using System;
using UnityEngine;

namespace StateMatching.Variable
{
    public class Vector3GroupController : VariableGroupContoller<Vector3Item, Vector3>
    {
        public override Type getGroupType()
        {
            return typeof(Vector3Group);
        }
    }
}

