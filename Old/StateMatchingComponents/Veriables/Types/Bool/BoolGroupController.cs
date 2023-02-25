using System;
using System.Collections;
using System.Collections.Generic;

namespace Nino.StateMatching.Variable
{
    public class BoolGroupController : VariableGroupContoller<bool>
    {
        public override Type getGroupType()
        {
            return typeof(BoolGroup);
        }

        public override Type getItemType()
        {
            return typeof(BoolItem);
        }
    }
}

