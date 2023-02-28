namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringAction_SetValue : ActionMethod
    {
        public StringAction_SetValue(SMSExecuter script, string actionName) : base(script, actionName)
        {
        }

        public override ActionInput CreateActionInput()
        {
            return new StringAction_String_Input(executer);
        }

        public override void PerformAction(ActionInput input)
        {
            StringAction_String_Input _input = input as StringAction_String_Input;
            Item getItem = executer.dataController.items.Find(x => x.itemName == _input.selectItem);
            StringValue getValue = getItem.value as StringValue;
            getValue.value = _input.inputValue;
        }
    }
}
