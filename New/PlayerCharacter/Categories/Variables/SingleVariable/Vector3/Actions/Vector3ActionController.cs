namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3ActionController : ActionController
    {
        public Vector3ActionController(SMSExecuter executer) : base(executer)
        {
            actions.Add(new Vector3Action_SetValue(executer, "Set Value"));
        }
    }
}
