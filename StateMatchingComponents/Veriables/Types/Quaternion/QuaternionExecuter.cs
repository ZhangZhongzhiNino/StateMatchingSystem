using System;
using UnityEngine;

namespace StateMatching.Variable
{
    public class QuaternionExecuter : VariableExecuter<Quaternion>
    {
        public override VariableItem<Quaternion> CreateNewItem()
        {
            return new QuaternionItem();
        }

        public override Type GetGroupControllerType()
        {
            return typeof(QuaternionGroupController);
        }
        public override Type GetGroupPreviewType()
        {
            return typeof(QuaternionGroupPreview);
        }
    }
}

