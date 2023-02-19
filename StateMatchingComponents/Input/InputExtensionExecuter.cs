using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Input
{
    public abstract class InputExtensionExecuter : ExtensionExecuter
    {
        public override CategoryController GetCategory()
        {
            return root.inputCategory;
        }
    }

}
