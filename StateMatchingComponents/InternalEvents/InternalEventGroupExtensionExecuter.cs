using StateMatching.Helper;

namespace StateMatching.InternalEvent
{
    public abstract class InternalEventGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController getCategory()
        {
            return root.internalEventController;
        }
    }
}

