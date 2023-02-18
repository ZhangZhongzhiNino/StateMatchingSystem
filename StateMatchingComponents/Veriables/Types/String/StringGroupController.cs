using System;

namespace StateMatching.Variable
{
    public class StringGroupController : VariableGroupContoller<string>
    {
        public override Type getGroupType()
        {
            return typeof(StringGroup);
        }

        public override Type getItemType()
        {
            return typeof(StringItem);
        }
    }
}

