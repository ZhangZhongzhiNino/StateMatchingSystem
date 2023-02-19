using Nino.StateMatching.Helper;

namespace Nino.StateMatching.StateMachine
{
    public abstract class StateMachineGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController getCategory()
        {
            return root.stateMachineCategory;
        }
    }
}

