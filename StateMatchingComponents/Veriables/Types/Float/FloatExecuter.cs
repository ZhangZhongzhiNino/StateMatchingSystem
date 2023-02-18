using System;
using UnityEngine;

namespace StateMatching.Variable
{
    public class FloatExecuter : VariableExecuter<float>
    {
        public override VariableItem<float> CreateNewItem()
        {
            return new FloatItem();
        }

        public override Type GetGroupControllerType()
        {
            return typeof(FloatGroupController);
        }

        public override Type GetGroupPreviewType()
        {
            return typeof(FloatGroupPreview);
        }
    }
}

