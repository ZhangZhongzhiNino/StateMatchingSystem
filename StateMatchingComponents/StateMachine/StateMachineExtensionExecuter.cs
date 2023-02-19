using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.StateMachine
{
    public class StateMachineExtensionExecuter : ExtensionExecuter
    {
        public override CategoryController getCategory()
        {
            return root.stateMachineCategory;
        }
    }
}

