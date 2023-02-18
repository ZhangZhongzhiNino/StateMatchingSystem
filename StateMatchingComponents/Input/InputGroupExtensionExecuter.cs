using StateMatching.Helper;

namespace StateMatching.Input
{
    public abstract class InputGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController getCategory()
        {
            return root.inputController;
        }
    }

}
