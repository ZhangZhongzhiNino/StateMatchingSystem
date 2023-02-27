using Sirenix.OdinInspector;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3Executer : VariableExecuter<Vector3Item,Vector3DataController>
    {
        [FoldoutGroup("Reference")]public Vector3ActionContainer actionContainer;
        protected override void InitilizeActionContainer()
        {
            actionContainer = new Vector3ActionContainer(this);
            actionContainerType = typeof(Vector3ActionContainer);
        }
        protected override string WriteLocalAddress()
        {
            return "Vector3";
        }
    }
}

