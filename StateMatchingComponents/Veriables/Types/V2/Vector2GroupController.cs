using System;
using UnityEngine;

namespace StateMatching.Variable
{
    public class Vector2GroupController : VariableGroupContoller<Vector2>
    {
        public override Type getGroupType()
        {
            return typeof(Vector2Group);
        }

        public override Type getItemType()
        {
            return typeof(Vector2Item);
        }
    }
}

