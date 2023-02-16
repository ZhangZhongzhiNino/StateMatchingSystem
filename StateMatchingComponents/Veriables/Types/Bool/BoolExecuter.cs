using System;
using UnityEngine;

namespace StateMatching.Variable
{
    public class BoolExecuter : VariableExecuter<BoolItem, bool>
    {
        public override Type GetGroupControllerType()
        {
            return typeof(BoolGroupController);
        }

        public override MonoBehaviour CreateNewItem()
        {
            return new BoolItem();
        }

        public override Type GetGroupPreviewType()
        {
            return typeof(BoolGroupPreview);
        }
    }
}

