namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2Action_SetValue : ActionMethod
    {
        public Vector2Action_SetValue(SMSExecuter script, string actionName) : base(script, actionName)
        {
        }

        public override ActionInput CreateActionInput()
        {
            return new Vector2Action_Vector2_Input(executer);
        }

        public override void PerformAction(ActionInput input)
        {
            Vector2Action_Vector2_Input _input = input as Vector2Action_Vector2_Input;
            Item getItem = executer.dataController.items.Find(x => x.itemName == _input.selectItem);
            Vector2Value getValue = getItem.value as Vector2Value;
            getValue.value = _input.inputValue;
        }
    }
}
