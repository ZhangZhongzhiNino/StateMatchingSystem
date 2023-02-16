using System;
using UnityEngine;

namespace StateMatching.Variable
{
    public class Vector3Executer : VariableExecuter<Vector3Item, Vector3>
    {
        public override Type GetGroupControllerType()
        {
            return typeof(Vector3GroupController);    
        }

        public override MonoBehaviour CreateNewItem()
        {
            return new Vector3Item();
        }

        public override Type GetGroupPreviewType()
        {
            return typeof(Vector3GroupPreview);
        }
    }
}

