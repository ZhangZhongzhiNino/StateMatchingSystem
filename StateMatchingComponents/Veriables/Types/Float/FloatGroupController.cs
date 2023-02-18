using System;

namespace StateMatching.Variable
{
    public class FloatGroupController : VariableGroupContoller<float>
    {
        public override Type getGroupType()
        {
            return typeof(FloatGroup);
        }

        public override Type getItemType()
        {
            return typeof(FloatItem);
        }
    }
}

