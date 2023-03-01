using UnityEngine;

namespace Nino.NewStateMatching
{
    public abstract class ExecuterInitializer : StateMatchingMonoBehaviourInitializer
    {
        protected ExecuterInitializer(StateMatchingMonoBehaviour creater, string name) : base(creater, name)
        {
        }

        public SMSExecuter executer { get => content as SMSExecuter; }
        public ExecuterGroup executerGroup { get => creater as ExecuterGroup; }        
        protected override void AssignContentParent() => executer.executerGroup = executerGroup;
        protected override void RemoveNullInCreaterAddress() => executerGroup.address.RemoveNullChildInChild();
        protected override void ResetHierarchy() => executerGroup.ResetHierarchy();
        protected override void UpdateCreaterAddress() => executerGroup.address.AddChild(executer.address);
        protected override string WriteAfterName() => ">";
        protected override string WriteBeforeName() => "<";
    }
}

