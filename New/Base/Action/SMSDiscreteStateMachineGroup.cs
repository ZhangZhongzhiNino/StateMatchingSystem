using Sirenix.OdinInspector;
namespace Nino.NewStateMatching
{
    public class SMSDiscreteStateMachineGroup : ExecuterGroup
    {
        public ExecuterInitializerCreater createDiscreteFSM;
        protected override void AddExecuterInitializers()
        {
            createDiscreteFSM = new ExecuterInitializerCreater(this, typeof(DiscreteStateMachineExecuter));
        }

        protected override string WriteLocalAddress()
        {
            return "Discrete State Machines";
        }
        
    }
}

