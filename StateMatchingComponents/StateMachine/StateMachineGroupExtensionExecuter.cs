using StateMatching.Helper;

namespace StateMatching.StateMachine
{
    public abstract class StateMachineGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController getCategory()
        {
            return root.stateMachineController;
        }
    }
}

