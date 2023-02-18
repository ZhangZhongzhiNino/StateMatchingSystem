using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.StateMachine
{
    public class StateMachineExtensionExecuter : ExtensionExecuter
    {
        public override CategoryController getCategory()
        {
            return root.stateMachineController;
        }
    }
}

