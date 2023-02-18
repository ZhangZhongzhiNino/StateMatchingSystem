using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.InternalEvent
{
    public class InternalEventExtrensionExecuter : ExtensionExecuter
    {
        public override CategoryController getCategory()
        {
            return root.internalEventController;
        }
    }
}

