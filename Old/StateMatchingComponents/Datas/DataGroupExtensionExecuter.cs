using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Data
{
    public abstract class DataGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController GetCategory()
        {
            return root.rootReferences.dataCategory;
        }
    }
}

