using System;
using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class QuaternionExtensionExecuter : VariableExtensionExecuter<Quaternion>
    {
        public override VariableItem<Quaternion> CreateNewItem()
        {
            return new QuaternionItem();
        }
        public override string GetActionGroupName()
        {
            return "Quaternion Variable";
        }
        public override Type GetGroupControllerType()
        {
            return typeof(QuaternionGroupController);
        }
        public override Type GetGroupPreviewType()
        {
            return typeof(QuaternionGroupPreview);
        }
        public override void InitiateActions()
        {

        }
        public override void EditModeUpdateCalls()
        {

        }
    }
}

