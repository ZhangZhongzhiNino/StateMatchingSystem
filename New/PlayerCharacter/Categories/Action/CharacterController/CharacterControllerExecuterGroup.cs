using System.Collections;
using System.Collections.Generic;

namespace Nino.NewStateMatching.Action
{
    public class CharacterControllerExecuterGroup : ExecuterGroup
    {
        protected override void AddExecuterInitializers()
        {
            GeneralUtility.AddExecuterInitializer(ref initializers, new ExecuterInitializer(this, "CC Movement", typeof(MovementExecuter)));
        }

        protected override string WriteLocalAddress()
        {
            return "Character Controller";
        }
    }
}

