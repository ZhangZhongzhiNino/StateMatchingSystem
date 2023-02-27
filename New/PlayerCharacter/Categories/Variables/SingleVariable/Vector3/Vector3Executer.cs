namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3Executer : VariableExecuter<Vector3Item,Vector3DataController>
    {
        protected override string WriteLocalAddress()
        {
            return "Vector3";
        }
    }
}

