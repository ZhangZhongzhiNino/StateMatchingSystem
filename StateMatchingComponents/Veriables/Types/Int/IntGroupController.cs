using System;

namespace StateMatching.Variable
{
    public class IntGroupController : VariableGroupContoller<int>
    {
        public override Type getGroupType()
        {
            return typeof(IntGroup);
        }

        public override Type getItemType()
        {
            return typeof(IntItem);
        }
    }
}

