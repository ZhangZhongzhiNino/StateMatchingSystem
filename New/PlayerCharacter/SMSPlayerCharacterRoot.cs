using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Nino.NewStateMatching.Trigger;
using Nino.NewStateMatching.Action;
namespace Nino.NewStateMatching
{
    public class SMSPlayerCharacterRoot : StateMatchingRoot
    {
        protected override void InitializeExecuterCategorys()
        {
            categories = new List<ExecuterCategory>();
            categories.Add(CreateCategory<TriggerCategory>("Trigger____"));
            categories.Add(CreateCategory<DetectorCategory>("Detector____"));
            categories.Add(CreateCategory<VariableCategory>("Variable____"));
            categories.Add(CreateCategory<DataCategory>("Data____"));
            categories.Add(CreateCategory<ActionCategory>("Action____"));
            categories.Add(CreateCategory<StateMachineCategory>("FSM____"));
        }


        
    }
}
