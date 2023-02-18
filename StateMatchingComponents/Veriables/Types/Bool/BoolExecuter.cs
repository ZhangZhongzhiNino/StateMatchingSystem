using System;
using UnityEngine;
using StateMatching.Helper;
namespace StateMatching.Variable
{
    public class BoolExecuter : VariableExecuter<bool>
    {
        public override VariableItem<bool> CreateNewItem()
        {
            return new BoolItem();
        }

        public override Type GetGroupControllerType()
        {
            return typeof(BoolGroupController);
        }

        public override Type GetGroupPreviewType()
        {
            return typeof(BoolGroupPreview);
        }
    }
}

