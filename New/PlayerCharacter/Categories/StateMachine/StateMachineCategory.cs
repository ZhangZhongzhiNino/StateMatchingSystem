namespace Nino.NewStateMatching.PlayerCharacter
{
    public class StateMachineCategory : ExecuterCategory
    {
        protected override void AddExecuterGroupInitializers()
        {
            GeneralUtility.AddGroupInitializer(ref initializers, new FSM_Group_Initializer(this, "FSM Group"));
        }
        protected override string WriteAddress()
        {
            return "State Machine";
        }
    }
}
