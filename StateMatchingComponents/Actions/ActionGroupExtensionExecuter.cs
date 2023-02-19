using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Action
{
    public abstract class ActionGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController getCategory()
        {
            return root.actionCategory;
        }
    }


}
