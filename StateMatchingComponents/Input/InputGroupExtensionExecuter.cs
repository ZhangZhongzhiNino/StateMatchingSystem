using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Input
{
    public abstract class InputGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController getCategory()
        {
            return root.inputCategory;
        }
    }

}
