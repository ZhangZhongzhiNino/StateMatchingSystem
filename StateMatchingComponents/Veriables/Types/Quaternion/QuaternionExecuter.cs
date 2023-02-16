using System;
using UnityEngine;

namespace StateMatching.Variable
{
    public class QuaternionExecuter : VariableExecuter<QuaternionItem, Quaternion>
    {
        public override Type GetGroupControllerType()
        {
            return typeof(QuaternionGroupController);
        }

        public override MonoBehaviour CreateNewItem()
        {
            return new QuaternionItem();
        }

        public override Type GetGroupPreviewType()
        {
            return typeof(QuaternionGroupPreview);
        }
    }
}

