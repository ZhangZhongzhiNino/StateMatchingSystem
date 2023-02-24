namespace Nino.NewStateMatching
{
    public class ExecuterGroupInitializer<T> : StateMatchingMonoBehaviourInitializer<T> where T : ExecuterGroup
    {
        protected override void AddCreaterReference()
        {
            content.executerCategory = creater as ExecuterCategory;
        }
    }
}

