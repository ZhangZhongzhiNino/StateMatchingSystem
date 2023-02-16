using System;
using System.Collections;
using System.Collections.Generic;

namespace StateMatching.Variable
{
    public class BoolGroupController : VariableGroupContoller<BoolItem, bool>
    {
        public override Type getGroupType()
        {
            return typeof(BoolGroup);
        }
    }
}

