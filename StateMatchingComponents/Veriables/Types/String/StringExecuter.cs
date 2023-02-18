using System;
using UnityEngine;

namespace StateMatching.Variable
{
    public class StringExecuter : VariableExecuter<string>
    {
        public override VariableItem<string> CreateNewItem()
        {
            return new StringItem();
        }

        public override Type GetGroupControllerType()
        {
            return typeof(StringGroupController);
        }
        public override Type GetGroupPreviewType()
        {
            return typeof(StringGroupPreview);
        }
    }
}

