using UnityEngine;

namespace Nino.NewStateMatching
{
    public abstract class ExecuterGroupInitializer : StateMatchingMonoBehaviourInitializer
    {
        protected ExecuterGroupInitializer(StateMatchingMonoBehaviour creater, string name) : base(creater, name)
        {
        }

        public ExecuterGroup executerGroup { get => content as ExecuterGroup; }
        public ExecuterCategory executerCategory { get => creater as ExecuterCategory; }
        
        protected override void AssignContentParent() => executerGroup.executerCategory = executerCategory;
        protected override void RemoveNullInCreaterAddress() => executerCategory.address.RemoveNullChildInChild();
        protected override void ResetHierarchy() => executerCategory.ResetHierarchy();
        protected override void UpdateCreaterAddress() => executerCategory.address.AddChild(executerGroup.address);
        protected override string WriteAfterName() => "";
        protected override string WriteBeforeName() => "____";
    }
}

