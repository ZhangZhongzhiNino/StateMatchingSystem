using Nino.NewStateMatching.Variable;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching
{
    public class VariableCategory : ExecuterCategory
    {
        protected override void AddExecuterGroupInitializers()
        {
            GeneralUtility.AddGroupInitializer(ref initializers, new ExecuterGroupInitializer(this,"Single Variable",typeof(SingleVariableExecuterGroup)));
        }

        protected override string WriteAddress()
        {
            return "Variable";
        }
    }
}
