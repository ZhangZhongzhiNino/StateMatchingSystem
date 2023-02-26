namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2Executer : OldVariableExecuter<Vector2Item, Vector2DataController>
    {
        protected override string WriteLocalAddress()
        {
            return "Vector2";
        }
    }
}

