namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class SingleVariableExecuterGroup : ExecuterGroup
    {
        public IntExecuterInitializer intExecuter;
        public FloatExecuterInitializer floatExecuter;
        public BoolExecuterInitializer boolExecuter;
        public StringExecuterInitializer stringExecuter;
        public Vector2ExecuterInitializer vector2Executer;
        public Vector3ExecuterInitializer vector3Executer;
        protected override void InitializeGroupedExecuterInitializers()
        {
            intExecuter = GeneralUtility.InitializeInitializer<IntExecuterInitializer, IntExecuter>(this);
            floatExecuter = GeneralUtility.InitializeInitializer<FloatExecuterInitializer, FloatExecuter>(this);
            boolExecuter = GeneralUtility.InitializeInitializer<BoolExecuterInitializer, BoolExecuter>(this);
            stringExecuter = GeneralUtility.InitializeInitializer<StringExecuterInitializer, StringExecuter>(this);
            vector2Executer = GeneralUtility.InitializeInitializer<Vector2ExecuterInitializer, Vector2Executer>(this);
            vector3Executer = GeneralUtility.InitializeInitializer<Vector3ExecuterInitializer, Vector3Executer>(this);
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