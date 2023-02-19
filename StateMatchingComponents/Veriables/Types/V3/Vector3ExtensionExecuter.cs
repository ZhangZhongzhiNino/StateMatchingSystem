using System;
using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class Vector3ExtensionExecuter : VariableExtensionExecuter<Vector3>
    {
        public override VariableItem<Vector3> CreateNewItem()
        {
            return new Vector3Item();
        }
        public override Type GetGroupControllerType()
        {
            return typeof(Vector3GroupController);    
        }
        public override Type GetGroupPreviewType()
        {
            return typeof(Vector3GroupPreview);
        }
    }
}

