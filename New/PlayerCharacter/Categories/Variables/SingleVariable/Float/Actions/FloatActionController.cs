using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatActionController : ActionController
    {
        public FloatActionController(SMSExecuter executer) : base(executer)
        {
            actions.Add(new FloatAction_SetValue(executer, "Set Value"));
            actions.Add(new FloatAction_Multiply(executer, "Multiply"));
            actions.Add(new FloatAction_Plus(executer, "Plus"));
        }
    }
}
