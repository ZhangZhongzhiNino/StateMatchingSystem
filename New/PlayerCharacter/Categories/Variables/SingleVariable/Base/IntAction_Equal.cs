namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntAction_Equal : VariableActionItem<IntItem, IntDataController,IntExecuter>
    {

        public int inputValue;

        public IntAction_Equal(IntExecuter script) : base(script)
        {
            actionName = "Assign New Value";
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
            i.value = inputValue;
        }
    }
}

