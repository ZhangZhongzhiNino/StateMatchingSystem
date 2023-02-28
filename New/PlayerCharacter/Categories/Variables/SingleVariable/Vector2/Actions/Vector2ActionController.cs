using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2ActionController : ActionController
    {
        public Vector2ActionController(SMSExecuter executer) : base(executer)
        {
            actions.Add(new Vector2Action_SetValue(executer, "Set Value"));
        }
    }
}
