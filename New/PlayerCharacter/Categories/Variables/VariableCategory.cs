using Nino.NewStateMatching.PlayerCharacter.Variable;

namespace Nino.NewStateMatching.PlayerCharacter
{
    public class VariableCategory : ExecuterCategory
    {
        public SingleVariableExecuterGroupInitializer singleVariableExecuterGroup;
        protected override void InitializeExecuterGroupInitializers()
        {
            singleVariableExecuterGroup = GeneralUtility.InitializeInitializer<SingleVariableExecuterGroupInitializer, SingleVariableExecuterGroup>(this);
        }

        protected override void RemoveExecuterGroups()
        {
        }

        protected override void RemoveExecuters()
        {
            
        }

        protected override string WriteAddress()
        {
            return "Variable";
        }
    }
}
