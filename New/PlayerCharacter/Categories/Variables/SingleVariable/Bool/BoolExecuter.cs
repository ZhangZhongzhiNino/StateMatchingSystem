using Sirenix.OdinInspector;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolExecuter : VariableExecuter<BoolItem,BoolDataController>
    {
        [FoldoutGroup("Reference")] public BoolActionContainer actionContainer;
        protected override void InitilizeActionContainer()
        {
            actionContainer = new BoolActionContainer(this);
            actionContainerType = typeof(BoolActionContainer);
        }
        protected override string WriteLocalAddress()
        {
            return "Bool";
        }
    }
}

