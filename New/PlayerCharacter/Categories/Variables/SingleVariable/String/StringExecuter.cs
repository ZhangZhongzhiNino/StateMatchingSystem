using Sirenix.OdinInspector;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringExecuter : VariableExecuter<StringItem, StringDataController>
    {
        [FoldoutGroup("Reference")] public StringActionContainer actionContainer;
        protected override void InitilizeActionContainer()
        {
            actionContainer = new StringActionContainer(this);
            actionContainerType = typeof(StringActionContainer);
        }
        protected override string WriteLocalAddress()
        {
            return "String";
        }
    }
}

