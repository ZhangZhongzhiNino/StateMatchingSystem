namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolAction_SetValue_Input : ItemSelectActionInput
    {
        public bool newValue = false;
        public BoolAction_SetValue_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
}


