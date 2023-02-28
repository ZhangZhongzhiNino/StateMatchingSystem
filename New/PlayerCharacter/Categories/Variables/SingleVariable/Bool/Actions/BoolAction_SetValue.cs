namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolAction_SetValue : ActionMethod
    {
        public BoolAction_SetValue(SMSExecuter script, string actionName) : base(script, actionName)
        {
        }
        public override ActionInput CreateActionInput()
        {
            return new BoolAction_SetValue_Input(executer);
        }
        public override void PerformAction(ActionInput input)
        {
            BoolAction_SetValue_Input _input = input as BoolAction_SetValue_Input;
            Item getItem = executer.dataController.items.Find(x => x.itemName == _input.selectItem);
            BoolValue getValue = getItem.value as BoolValue;
            getValue.value = _input.newValue;

        }
    }
}


