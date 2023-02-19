using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Variable
{
    public class VariableExtensionExecuter : ExtensionExecuter
    {
        public override CategoryController getCategory()
        {
            return root.variableCategory;
        }
    }
}

