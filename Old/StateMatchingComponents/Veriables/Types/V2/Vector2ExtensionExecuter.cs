using System;
using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class Vector2ExtensionExecuter : VariableExtensionExecuter<Vector2>
    {
        public override VariableItem<Vector2> CreateNewItem()
        {
            return new Vector2Item();
        }
        public override string GetActionGroupName()
        {
            return "Vector2 Variable";
        }
        public override Type GetGroupControllerType()
        {
            return typeof(Vector2GroupController);
        }
        public override Type GetGroupPreviewType()
        {
            return typeof(Vector2GroupPreview);
        }
        public override void InitiateActions()
        {

        }
        public override void EditModeUpdateCalls()
        {

        }
    }
}

