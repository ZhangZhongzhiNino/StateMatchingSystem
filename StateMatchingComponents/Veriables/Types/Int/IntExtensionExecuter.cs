using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class IntExtensionExecuter : VariableExtensionExecuter<int>
    {
        public override VariableItem<int> CreateNewItem()
        {
            return new IntItem();
        }

        public override Type GetGroupControllerType()
        {
            return typeof(IntGroupController);
        }

        public override Type GetGroupPreviewType()
        {
            return typeof(IntGroupPreview);
        }
    }
}

