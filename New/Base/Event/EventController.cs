using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching
{
    public class EventController
    {
        public List<EventInfo> eventInfos;

    }
    public class EventInfo
    {
        public string eventName;
        public UnityEvent eventReference;
        public void AddEventListener(UnityAction action)
        {
            eventReference.AddListener(action);
        }
    }
}

