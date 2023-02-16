using System;
using UnityEngine;

namespace StateMatching.Variable
{
    public class Vector2GroupController : VariableGroupContoller<Vector2Item, Vector2>
    {
        public override Type getGroupType()
        {
            return typeof(Vector2Group);
        }
    }
}

