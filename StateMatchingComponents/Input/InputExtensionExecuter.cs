using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Input
{
    public class InputExtensionExecuter : ExtensionExecuter
    {
        public override CategoryController getCategory()
        {
            return root.inputCategory;
        }
    }

}
