using StateMatching.Helper;

namespace StateMatching.Action
{
    public abstract class ActionGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController getCategory()
        {
            return root.actionController;
        }
    }


}
