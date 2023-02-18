using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
using System;

namespace StateMatching.Action
{
    public class ActionExtensionExecuter : ExtensionExecuter
    {
        public override CategoryController getCategory()
        {
            return root.actionController;
        }
    }


}
