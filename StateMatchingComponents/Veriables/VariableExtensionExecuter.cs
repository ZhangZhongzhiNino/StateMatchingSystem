using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Variable
{
    public abstract class VariableExtensionExecuter : ExtensionExecuter
    {
        public override CategoryController GetCategory()
        {
            return root.variableCategory;
        }
    }
}

