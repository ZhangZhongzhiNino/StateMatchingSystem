using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
using System;

namespace Nino.StateMatching.Action
{
    public class ActionExtensionExecuter : ExtensionExecuter
    {
        public override CategoryController getCategory()
        {
            return root.actionCategory;
        }
    }


}
