using Nino.NewStateMatching.PlayerCharacter.Variable;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter
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
