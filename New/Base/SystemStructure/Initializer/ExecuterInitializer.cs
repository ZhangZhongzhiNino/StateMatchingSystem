namespace Nino.NewStateMatching
{
    public abstract class ExecuterInitializer<T> : StateMatchingMonoBehaviourInitializer<T> where T : SMSExecuter
    {
        public override void Create()
        {
            base.Create();
            ExecuterGroup _creater = creater as ExecuterGroup;
            content.executerGroup = _creater;
            _creater.ResetHierarchy();
            _creater.address.AddChild(content.address);
        }
        public override void Remove()
        {
            base.Remove();
            ExecuterGroup _creater = creater as ExecuterGroup;
            _creater.address.RemoveNullChildInChild();
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

