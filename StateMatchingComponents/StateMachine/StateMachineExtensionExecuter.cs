using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.StateMachine
{
    public abstract class StateMachineExtensionExecuter : ExtensionExecuter
    {
        public override CategoryController GetCategory()
        {
            return root.stateMachineCategory;
        }
    }
}

