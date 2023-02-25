namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringItem : VariableItem<string>
    {
        protected override void InitializeInstance()
        {
            value = "";
        }
    }
}

