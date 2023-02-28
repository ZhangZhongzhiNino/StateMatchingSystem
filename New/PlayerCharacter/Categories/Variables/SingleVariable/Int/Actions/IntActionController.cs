using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntActionController : ActionController
    {
        public IntActionController(SMSExecuter executer) : base(executer)
        {
            actions.Add(new IntAction_SetValue(executer, "Set Value"));
            actions.Add(new IntAction_Plus(executer, "Plus"));
        }
    }
}
