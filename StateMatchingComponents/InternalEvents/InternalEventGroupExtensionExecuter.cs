using Nino.StateMatching.Helper;

namespace Nino.StateMatching.InternalEvent
{
    public abstract class InternalEventGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController getCategory()
        {
            return root.internalEventCategory;
        }
    }
}

