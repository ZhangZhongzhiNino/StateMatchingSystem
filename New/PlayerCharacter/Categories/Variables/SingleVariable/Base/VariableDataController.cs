namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public abstract class VariableDataController<Item, Collection> : DataController<Item, Collection>
        where Item : NewStateMatching.Item
        where Collection : NewStateMatching.Collection<Item>
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

