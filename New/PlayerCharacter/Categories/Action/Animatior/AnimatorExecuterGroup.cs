using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nino.NewStateMatching.Action.Animator
{
    public class AnimatorExecuterGroup : ExecuterGroup
    {
        protected override void AddExecuterInitializers()
        {
            GeneralUtility.AddExecuterInitializer(ref initializers, new ExecuterInitializer(this, "Root Animator", typeof(RootAnimatorExecuter)));
        }

        protected override string WriteLocalAddress()
        {
            return "Animator";
        }
    }

}
