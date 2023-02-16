using System;
using UnityEngine;

namespace StateMatching.Variable
{
    public class FloatExecuter : VariableExecuter<FloatItem, float>
    {
        public override Type GetGroupControllerType()
        {
            return typeof(FloatGroupController);
        }

        public override MonoBehaviour CreateNewItem()
        {
            return new FloatItem();
        }

        public override Type GetGroupPreviewType()
        {
            return typeof(FloatGroupPreview);
        }
    }
}

