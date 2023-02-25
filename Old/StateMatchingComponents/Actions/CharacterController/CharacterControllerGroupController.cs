using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
using System;

namespace Nino.StateMatching.Action
{
    public class CharacterControllerGroupController : GroupController<CharacterController>
    {
        public override Type getGroupType()
        {
            return typeof(CharacterControllerGroup);
        }

        public override Type getItemType()
        {
            return typeof(CharacterControllerItem);
        }
    }

}
