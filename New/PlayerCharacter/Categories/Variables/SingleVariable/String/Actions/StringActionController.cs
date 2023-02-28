using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringActionController : ActionController
    {
        public StringActionController(SMSExecuter executer) : base(executer)
        {
            actions.Add(new StringAction_SetValue(executer, "Set Value"));
        }
    }
}
