using Nino.NewStateMatching.Action.Animator;
using System.Collections.Generic;

namespace Nino.NewStateMatching.Action
{
    public class ActionCategory : ExecuterCategory
    {
        protected override void AddExecuterGroupInitializers()
        {
            GeneralUtility.AddGroupInitializer(ref initializers, new ExecuterGroupInitializer(this, "Animator", typeof(AnimatorExecuterGroup)));
            GeneralUtility.AddGroupInitializer(ref initializers, new ExecuterGroupInitializer(this, "Camera", typeof(CameraExecuterGroup)));
            GeneralUtility.AddGroupInitializer(ref initializers, new ExecuterGroupInitializer(this, "Character Controller", typeof(CharacterControllerExecuterGroup)));
        }

        protected override string WriteAddress()
        {
            return "Action";
        }
    }
}
