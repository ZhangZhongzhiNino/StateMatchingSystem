using StateMatching.Helper;

namespace StateMatching.Variable
{
    public abstract class VariableGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController getCategory()
        {
            return root.variableController;
        }
    }
}

