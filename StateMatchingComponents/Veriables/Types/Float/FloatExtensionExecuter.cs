using System;
using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class FloatExtensionExecuter : VariableExtensionExecuter<float>
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

