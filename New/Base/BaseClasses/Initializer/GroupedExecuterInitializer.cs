namespace Nino.NewStateMatching
{
    public class GroupedExecuterInitializer<T> : StateMatchingMonoBehaviourInitializer<T> where T : GroupedDataExecuter
    {
        protected override void AddCreaterReference()
        {
            content.executerGroup = creater as ExecuterGroup;
        }
    }
}

