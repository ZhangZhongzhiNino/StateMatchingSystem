namespace Nino.NewStateMatching
{
    public abstract class ExecuterGroupInitializer<T> : StateMatchingMonoBehaviourInitializer<T> where T : ExecuterGroup
    {
        protected override void AddCreaterReferenceResetHierarchy()
        {
            ExecuterCategory _creater = creater as ExecuterCategory;
            content.executerCategory = _creater;
            _creater.ResetHierarchy();
        }

        protected override string WriteAfterName()
        {
            return "";
        }

        protected override string WriteBeforeName()
        {
            return "____";
        }
    }
}

