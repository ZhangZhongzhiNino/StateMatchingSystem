using Nino.StateMatching.Helper;

namespace Nino.StateMatching.InternalEvent
{
    public abstract class InternalEventGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController GetCategory()
        {
            return root.rootReferences.internalEventCategory;
        }
    }
}

