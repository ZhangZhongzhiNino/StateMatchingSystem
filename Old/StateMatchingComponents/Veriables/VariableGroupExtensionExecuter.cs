using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Variable
{
    public abstract class VariableGroupExtensionExecuter<V> : GroupExtensionExecuter<V>
    {
        public override CategoryController GetCategory()
        {
            return root.rootReferences.variableCategory;
        }
    }
}

