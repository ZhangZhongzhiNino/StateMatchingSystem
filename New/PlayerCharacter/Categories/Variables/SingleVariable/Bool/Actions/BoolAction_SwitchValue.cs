namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolAction_SwitchValue : ActionMethod
    {
        public BoolAction_SwitchValue(SMSExecuter script, string actionName) : base(script, actionName)
        {
        }
        public override ActionInput CreateActionInput()
        {
            return new ItemSelectActionInput(executer);
        }

        public override void PerformAction(ActionInput input)
        {
            ItemSelectActionInput _input = input as ItemSelectActionInput;
            Item getItem = executer.dataController.items.Find(x => x.itemName == _input.selectItem);
            BoolValue getValue = getItem.value as BoolValue;
            getValue.value = !getValue.value;
        }
    }
}


