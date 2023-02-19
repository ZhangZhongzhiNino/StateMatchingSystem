using System;
using UnityEngine;
using Nino.StateMatching.Helper;
namespace Nino.StateMatching.Variable
{
    public class BoolExtensionExecuter : VariableExtensionExecuter<bool>
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

