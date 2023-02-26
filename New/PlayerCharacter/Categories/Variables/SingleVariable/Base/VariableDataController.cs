namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public abstract class VariableDataController<Item> : DataController
        where Item : NewStateMatching.Item, new()
    {
        protected override void InitializeInstance()
        {
            
        }
        protected override string WriteHint()
        {
            return "";
        }
    }
}

