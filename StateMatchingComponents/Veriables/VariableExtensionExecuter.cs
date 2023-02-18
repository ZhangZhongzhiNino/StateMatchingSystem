using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.Variable
{
    public class VariableExtensionExecuter : ExtensionExecuter
    {
        public override CategoryController getCategory()
        {
            return root.variableController;
        }
    }
}

