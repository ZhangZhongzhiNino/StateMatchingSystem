namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class SingleVariableExecuterGroup : ExecuterGroup
    {
        
        protected override void AddExecuterInitializers()
        {
            GeneralUtility.AddExecuterInitializer(ref initializers, new IntExecuterInitializer(this, "Int"));
            GeneralUtility.AddExecuterInitializer(ref initializers, new FloatExecuterInitializer(this, "Float"));
            GeneralUtility.AddExecuterInitializer(ref initializers, new BoolExecuterInitializer(this, "Bool"));
            GeneralUtility.AddExecuterInitializer(ref initializers, new StringExecuterInitializer(this, "String"));
            GeneralUtility.AddExecuterInitializer(ref initializers, new Vector2ExecuterInitializer(this, "Vector2"));
            GeneralUtility.AddExecuterInitializer(ref initializers, new Vector3ExecuterInitializer(this, "Vector3"));
        }

        protected override void RemoveExecuters()
        {

        }

        protected override string WriteLocalAddress()
        {
            return "Single Variable";
        }
    }
}