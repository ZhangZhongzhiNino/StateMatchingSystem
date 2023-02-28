namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntAction_Plus : ActionMethod
    {
        public IntAction_Plus(SMSExecuter script, string actionName) : base(script, actionName)
        {
        }
        public override ActionInput CreateActionInput()
        {
            return new IntAction_Int_Input(executer);
        }
        public override void PerformAction(ActionInput input)
        {
            IntAction_Int_Input _input = input as IntAction_Int_Input;
            Item getItem = executer.dataController.items.Find(x => x.itemName == _input.selectItem);
            IntValue getValue = getItem.value as IntValue;
            getValue.value += _input.inputValue;
        }
    }
}
