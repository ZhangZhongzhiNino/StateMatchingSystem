using Nino.StateMatching.Helper;

namespace Nino.StateMatching.StateMachine
{
    public abstract class StateMachineGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController GetCategory()
        {
            return root.stateMachineCategory;
        }
    }
}

