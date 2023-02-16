using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMatching.Variable
{
    public class IntExecuter : VariableExecuter<IntItem, int>
    {
        public override Type GetGroupControllerType()
        {
            return typeof(IntGroupController);
        }

        public override MonoBehaviour CreateNewItem()
        {
            return new IntItem();
        }

        public override Type GetGroupPreviewType()
        {
            return typeof(IntGroupPreview);
        }
    }
}

