using System.Collections.Generic;

namespace Nino.NewStateMatching
{
    public class StateMatchingGlobalReference : StateMatchingMonoBehaviour
    {
        public List<StateMatchingRoot> references;
        public override void Initialize()
        {
            if(references == null) references = new List<StateMatchingRoot>();
        }
        public override void Remove()
        {
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
    }
}

