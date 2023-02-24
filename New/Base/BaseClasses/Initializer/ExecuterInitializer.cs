using System.Collections;
using System;

namespace Nino.NewStateMatching
{
    public class ExecuterInitializer<T> : StateMatchingMonoBehaviourInitializer<T> where T : DataExecuter
    {
        protected override void AddCreaterReference()
        {
            content.executerCategory = creater as ExecuterCategory;
        }
    }
}

