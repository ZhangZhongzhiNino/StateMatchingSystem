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
        public EventController()
        {
            eventInfos = new List<EventInfo>();
        }
        public void InvokeEvent(string eventName)
        {
            EventInfo getEvent = eventInfos.Find(x => x.eventName == eventName);
            getEvent.eventReference?.Invoke();
        }
    }
    public class EventInfo
    {
        public string eventName;
        public UnityEvent eventReference;
        public EventInfo(string eventName)
        {
            this.eventName = eventName;
            eventReference = new UnityEvent();
        }
    }
}

