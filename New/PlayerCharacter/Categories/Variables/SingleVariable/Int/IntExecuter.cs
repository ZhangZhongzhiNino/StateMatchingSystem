using Sirenix.OdinInspector;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntExecuter : VariableExecuter<IntItem,IntDataController>
    {
        [FoldoutGroup("Reference")] public IntActionContainer actionContainer;
        protected override void InitilizeActionContainer()
        {
            actionContainer = new IntActionContainer(this);
            actionContainerType = typeof(IntActionContainer);
        }

        protected override string WriteLocalAddress()
        {
            return "Int";
        }
    }
}

