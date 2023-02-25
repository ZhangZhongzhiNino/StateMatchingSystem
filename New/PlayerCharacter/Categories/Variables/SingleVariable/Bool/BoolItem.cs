namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolItem : VariableItem<bool>
    {
        protected override void InitializeInstance()
        {
            value = false;
        }
    }
}

