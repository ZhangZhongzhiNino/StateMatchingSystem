using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
using UnityEngine.Events;
using Sirenix.OdinInspector;
namespace StateMatching.InternalEvent
{
    public class UnityAnimationEvent : MonoBehaviour, IGroupItem<UnityAnimationEvent, UnityAnimationEvent>
    {
        public string eventName;
        public string itemName { get { return eventName; } set { eventName = value; } }

        public AnimationEvent animationEvent;
        public UnityEvent unityEvent;
        public UnityAnimationEvent value { get { return this; } set { AssignItem(value); } }
        public void AssignItem(UnityAnimationEvent item)
        {
            this.eventName = item.eventName;
            this.animationEvent = item.animationEvent;
        }
        
        public void InvokeUnityEvent()
        {
            unityEvent?.Invoke();
        }


        public UnityAnimationEvent(string eventName, AnimationEvent animationEvent)
        {
            this.eventName = eventName;
            this.animationEvent = animationEvent;
            this.unityEvent = new UnityEvent();
        }
    }
}

