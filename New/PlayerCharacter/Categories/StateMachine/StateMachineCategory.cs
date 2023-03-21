namespace Nino.NewStateMatching
{
    public class StateMachineCategory : ExecuterCategory
    {
        protected override void AddExecuterGroupInitializers()
        {
            GeneralUtility.AddGroupInitializer(ref initializers, new ExecuterGroupInitializer(this, "FSM Group",typeof(SMSDiscreteStateMachineGroup)));
        }
        protected override string WriteAddress()
        {
            return "State Machine";
        }
    }
}
