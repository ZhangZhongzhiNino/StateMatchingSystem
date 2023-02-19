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
        public override string GetActionGroupName()
        {
            return "Bool Variable";
        }
        public override Type GetGroupControllerType()
        {
            return typeof(BoolGroupController);
        }
        public override Type GetGroupPreviewType()
        {
            return typeof(BoolGroupPreview);
        }

        public override void InitiateActions()
        {

        }
        public override void EditModeUpdateCalls()
        {

        }
    }
}

