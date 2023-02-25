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
        public override string GetActionGroupName()
        {
            return "Float Variable";
        }
        public override Type GetGroupControllerType()
        {
            return typeof(FloatGroupController);
        }
        public override Type GetGroupPreviewType()
        {
            return typeof(FloatGroupPreview);
        }

        public override void InitiateActions()
        {
            
        }
        public override void EditModeUpdateCalls()
        {

        }
    }
}

