using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
using UnityEngine.Events;
using Sirenix.OdinInspector;
namespace StateMatching.InternalEvent
{
    public class UnityAnimationEventItem : Item<UnityAnimationEventItem>
    {


        public AnimationEvent animationEvent;
        public UnityEvent unityEvent;

        public override UnityAnimationEventItem value 
        {
            get { return this; }
            set { AssignItem(value); }
        }

        public void InvokeUnityEvent()
        {
            unityEvent?.Invoke();
        }

        public override void AssignItem(Item<UnityAnimationEventItem> item)
        {
            UnityAnimationEventItem _item = item as UnityAnimationEventItem;
            base.AssignItem(item);
            animationEvent = _item.animationEvent;
            unityEvent = _item.unityEvent;
        }
        public UnityAnimationEventItem(string eventName, AnimationEvent animationEvent)
        {
            this.itemName = eventName;
            this.animationEvent = animationEvent;
            this.unityEvent = new UnityEvent();
        }
    }
}

