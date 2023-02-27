using Sirenix.OdinInspector;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatExecuter : VariableExecuter<FloatItem,FloatDataController>
    {
        [FoldoutGroup("Reference")] public FloatActionContainer actionContainer;
        protected override void InitilizeActionContainer()
        {
            actionContainer = new FloatActionContainer(this);
            actionContainerType = typeof(FloatActionContainer);
        }
        protected override string WriteLocalAddress()
        {
            return "Float";
        }
    }
}

