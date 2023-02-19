using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Variable
{
    public abstract class VariableGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController getCategory()
        {
            return root.variableCategory;
        }
    }
}

