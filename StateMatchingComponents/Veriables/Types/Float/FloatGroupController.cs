using System;

namespace StateMatching.Variable
{
    public class FloatGroupController : VariableGroupContoller<FloatItem, float>
    {
        public override Type getGroupType()
        {
            return typeof(FloatGroup);
        }
    }
}

