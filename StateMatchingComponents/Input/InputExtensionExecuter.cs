using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.Input
{
    public class InputExtensionExecuter : ExtensionExecuter
    {
        public override CategoryController getCategory()
        {
            return root.inputController;
        }
    }

}
