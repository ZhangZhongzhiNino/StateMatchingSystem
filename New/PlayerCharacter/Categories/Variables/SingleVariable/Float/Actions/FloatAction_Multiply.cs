namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatAction_Multiply : ActionMethod
    {
        public FloatAction_Multiply(SMSExecuter script, string actionName) : base(script, actionName)
        {
        }

        public override ActionInput CreateActionInput()
        {
            return new FloatAction_Float_Input(executer);
        }

        public override void PerformAction(ActionInput input)
        {
            FloatAction_Float_Input _input = input as FloatAction_Float_Input;
            Item getItem = executer.dataController.items.Find(x => x.itemName == _input.selectItem);
            FloatValue getValue = getItem.value as FloatValue;
            getValue.value *= _input.inputValue;
        }
    }
}
