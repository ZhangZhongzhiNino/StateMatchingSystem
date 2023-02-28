using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{

    public class BoolActionController : ActionController
    {
        public BoolActionController(SMSExecuter executer) : base(executer)
        {
            actions.Add(new BoolAction_SetValue(executer,"Vet Value"));
            actions.Add(new BoolAction_SwitchValue(executer, "Switch Value"));
        }
    }
}


