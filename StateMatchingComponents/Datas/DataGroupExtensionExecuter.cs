using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Data
{
    public abstract class DataGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController getCategory()
        {
            return root.dataCategory;
        }
    }
}

