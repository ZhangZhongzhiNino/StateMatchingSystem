namespace Nino.NewStateMatching
{
    public abstract class ExecuterGroupInitializer<T> : StateMatchingMonoBehaviourInitializer<T> where T : ExecuterGroup
    {
        public override void Create()
        {
            base.Create();
            ExecuterCategory _creater = creater as ExecuterCategory;
            content.executerCategory = _creater;
            _creater.address.AddChild(content.address);
            _creater.ResetHierarchy();

        }
        public override void Remove()
        {
            base.Remove();
            ExecuterCategory _creater = creater as ExecuterCategory;
            _creater.address.RemoveNullChildInChild();

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

