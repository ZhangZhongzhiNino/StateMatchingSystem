using System;
using UnityEngine;

namespace StateMatching.Variable
{
    public class Vector2Executer : VariableExecuter<Vector2>
    {
        public override VariableItem<Vector2> CreateNewItem()
        {
            return new Vector2Item();
        }

        public override Type GetGroupControllerType()
        {
            return typeof(Vector2GroupController);
        }


        public override Type GetGroupPreviewType()
        {
            return typeof(Vector2GroupPreview);
        }
    }
}

