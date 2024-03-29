using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Nino.StateMatching.Helper;
using System;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace Nino.StateMatching.InternalEvent
{
    public class UnityAnimationEventExtensionExecuter : InternalEventGroupExtensionExecuter<UnityAnimationEventItem>
    {
        [FoldoutGroup("Reference")] public UnityEngine.Animator animator;
        
        [Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f),PropertyOrder(-100)]
        void UpdateAnimationEvents()
        {
            if (animator == null || groupController == null) return;
            groupController.RemoveAllInItems(items => items.value == null || items.value.animationEvent == null);
            UnityEditor.Animations.AnimatorController animatorController;
            animatorController = animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;

            if (animatorController != null)
            {
                foreach (AnimationClip clip in animatorController.animationClips)
                {
                    string clipName = clip.name;
                    AnimationEvent[] animEvents = AnimationUtility.GetAnimationEvents(clip);
                    if (animEvents == null || animEvents.Length == 0) return;
                    AnimationEvent[] newAnimEvents = new AnimationEvent[animEvents.Length];
                    for (int i = 0; i < animEvents.Length; i++)
                    {
                        newAnimEvents[i] = new AnimationEvent();
                        newAnimEvents[i].time = animEvents[i].time;
                        newAnimEvents[i].functionName = "AnimationEventHaldler";
                        newAnimEvents[i].messageOptions = animEvents[i].messageOptions;
                        newAnimEvents[i].objectReferenceParameter = animEvents[i].objectReferenceParameter;
                        newAnimEvents[i].floatParameter = animEvents[i].floatParameter;
                        newAnimEvents[i].intParameter = i;
                        newAnimEvents[i].stringParameter = animEvents[i].stringParameter;
                    }
                    AnimationUtility.SetAnimationEvents(clip, newAnimEvents);
                    for(int i = 0; i < newAnimEvents.Length; i++)
                    {
                        string animEventName = "#" + i.ToString() + " Event in: " + clipName;
                        UnityAnimationEventItem newAnimationEvent = new UnityAnimationEventItem(animEventName, newAnimEvents[i]);
                        groupController.AddItem(animEventName, newAnimationEvent, clipName);
                    }
                }
            }
        }
        public bool autoUpdate = true;

        #region Edit Events
        List<string> groupNames { get { return groupPreview.groupNames; } }
        [FoldoutGroup("Edit Events")]
        [ValueDropdown("groupNames")]
        public string selectGroup;
        (List<UnityEvent>, List<string>) GetSelectedEvents()
        {
            List<UnityEvent> newEvents = new List<UnityEvent>();
            List<string> newNames = new List<string>();
            UnityAnimationEventGroup group = groupController.GetGroup(selectGroup) as UnityAnimationEventGroup;
            if (group == null) return (null, null);
            foreach (UnityAnimationEventItem animEvent in group.items)
            {
                newEvents.Add(animEvent.unityEvent);
                newNames.Add(animEvent.itemName);
            }
            return (newEvents, newNames);
        }
        [FoldoutGroup("Edit Events")]
        [ShowInInspector]
        [ListDrawerSettings(OnBeginListElementGUI = "DrawEventNames", DraggableItems = false)]
        List<UnityEvent> events
        {
            get
            {
                var r = GetSelectedEvents();
                return r.Item1;
            }
            set { }
        }
        List<string> eventNames
        {
            get
            {
                var r = GetSelectedEvents();
                return r.Item2;
            }
        }
        void DrawEventNames(int index)
        {
            if (index >= 0 && index < eventNames.Count && index < events.Count)
            {
                GUI.Label(GUILayoutUtility.GetRect(80, 30), eventNames[index]);
            }
        }


        #endregion
        public override Type GetGroupControllerType()
        {
            return typeof(UnityAnimationEventGroupController);
        }
        public override Type GetGroupPreviewType()
        {
            return typeof(UnityAnimationEventGroupPreview);
        }
        public override string GetActionGroupName()
        {
            return "Animation Event";
        }
        public override void InitiateActions()
        {

        }
        public override void EditModeUpdateCalls()
        {
            if (autoUpdate) UpdateAnimationEvents();
        }
        public override void Initiate<_T>(_T instance = null, StateMatchingRoot stateMatchingRoot = null)
        {
            base.Initiate(instance, stateMatchingRoot);
            animator = root.animator;
        }
    }

}
