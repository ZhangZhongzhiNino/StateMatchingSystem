namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntAction_PlusEqual : VariableActionItem<IntItem, IntDataController, IntExecuter>
    {

        public int inputValue;

        public IntAction_PlusEqual(IntExecuter script) : base(script)
        {
            actionName = "Value += new Value";
        }
        public override void AssignVariable(ActionItemBase<IntExecuter> instance)
        {
            IntAction_Equal _instance = instance as IntAction_Equal;
            selectItem = _instance.selectItem;
            inputValue = _instance.inputValue;
        }

        public override void PerformAction()
        {
            IntItem i = script?.dataController?.items.Find(x => x.itemName == selectItem);
            i.value += inputValue;
        }
    }
}

