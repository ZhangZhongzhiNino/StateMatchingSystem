namespace Nino.NewStateMatching
{
    public abstract class ExecuterInitializer<T> : StateMatchingMonoBehaviourInitializer<T> where T : SMSExecuter
    {
        protected override void AddCreaterReferenceResetHierarchy()
        {
            ExecuterGroup _creater = creater as ExecuterGroup;
            content.executerGroup = _creater;
            _creater.ResetHierarchy();
        }

        protected override string WriteAfterName()
        {
            return ">";
        }

        protected override string WriteBeforeName()
        {
            return "<";
        }
    }
}

