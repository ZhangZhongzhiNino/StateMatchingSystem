using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.InternalEvent
{
    public abstract class InternalEventExtrensionExecuter : ExtensionExecuter
    {
        public override CategoryController GetCategory()
        {
            return root.rootReferences.internalEventCategory;
        }
    }
}

