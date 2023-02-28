namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3Action_SetValue : ActionMethod
    {
        public Vector3Action_SetValue(SMSExecuter script, string actionName) : base(script, actionName)
        {
        }

        public override ActionInput CreateActionInput()
        {
            return new Vector3Action_Vector3_Input(executer);
        }

        public override void PerformAction(ActionInput input)
        {
            Vector3Action_Vector3_Input _input = input as Vector3Action_Vector3_Input;
            Item getItem = executer.dataController.items.Find(x => x.itemName == _input.selectItem);
            Vector3Value getValue = getItem.value as Vector3Value;
            getValue.value = _input.inputValue;
        }
    }
}
