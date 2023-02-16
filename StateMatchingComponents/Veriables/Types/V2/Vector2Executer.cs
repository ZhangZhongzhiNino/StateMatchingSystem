using System;
using UnityEngine;

namespace StateMatching.Variable
{
    public class Vector2Executer : VariableExecuter<Vector2Item, Vector2>
    {
        public override Type GetGroupControllerType()
        {
            return typeof(Vector2GroupController);
        }

        public override MonoBehaviour CreateNewItem()
        {
            return new Vector2Item();
        }

        public override Type GetGroupPreviewType()
        {
            return typeof(Vector2GroupPreview);
        }
    }
}

