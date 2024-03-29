﻿using System;
using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class StringExtensionExecuter : VariableExtensionExecuter<string>
    {
        public override VariableItem<string> CreateNewItem()
        {
            return new StringItem();
        }
        public override string GetActionGroupName()
        {
            return "String Variable";
        }
        public override Type GetGroupControllerType()
        {
            return typeof(StringGroupController);
        }
        public override Type GetGroupPreviewType()
        {
            return typeof(StringGroupPreview);
        }
        public override void InitiateActions()
        {

        }
        public override void EditModeUpdateCalls()
        {

        }
    }
}

