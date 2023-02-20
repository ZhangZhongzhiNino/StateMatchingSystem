using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
using System;

namespace Nino.StateMatching.Action
{
    public abstract class ActionExtensionExecuter : ExtensionExecuter
    {
        
        public override CategoryController GetCategory()
        {
            return root.rootReferences.actionCategory;
        }
    }


}
