using Sirenix.OdinInspector;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2Executer : VariableExecuter<Vector2Item,Vector2DataController>
    {
        [FoldoutGroup("Reference")] public Vector2ActionContainer actionContainer;
        protected override void InitilizeActionContainer()
        {
            actionContainer = new Vector2ActionContainer(this);
            actionContainerType = typeof(Vector2ActionContainer);
        }
        protected override string WriteLocalAddress()
        {
            return "Vector2";
        }
    }
}

