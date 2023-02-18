using StateMatching.Helper;

namespace StateMatching.Data
{
    public abstract class DataGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController getCategory()
        {
            return root.dataController;
        }
    }
}

