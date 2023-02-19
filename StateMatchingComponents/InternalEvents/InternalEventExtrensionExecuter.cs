using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.InternalEvent
{
    public class InternalEventExtrensionExecuter : ExtensionExecuter
    {
        public override CategoryController getCategory()
        {
            return root.internalEventCategory;
        }
    }
}

