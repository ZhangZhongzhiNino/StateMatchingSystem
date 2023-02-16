using System;

namespace StateMatching.Variable
{
    public class IntGroupController : VariableGroupContoller<IntItem, int>
    {
        public override Type getGroupType()
        {
            return typeof(IntGroup);
        }
    }
}

