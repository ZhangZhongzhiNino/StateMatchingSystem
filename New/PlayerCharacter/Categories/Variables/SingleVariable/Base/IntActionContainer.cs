namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntActionContainer : ActionContainer<IntExecuter>
    {
        public IntActionContainer(IntExecuter script) : base(script)
        {
            actions.Add(new IntAction_Equal(script));
            actions.Add(new IntAction_PlusEqual(script));
        }
    }
}

