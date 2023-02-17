using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using StateMatching.Helper;
using System;
using Sirenix.OdinInspector;

namespace StateMatching.InternalEvent
{
    public class UnityAnimationEventHandler : GroupExtensionExecuter<UnityAnimationEvent, UnityAnimationEvent>
    {
        public UnityEngine.Animator animator;
        public override Type GetGroupControllerType()
        {
            return typeof(UnityAnimationEventGroupController);
        }
        public override Type GetGroupPreviewType()
        {
            return typeof(UnityAnimationEventGroupPreview);
        }
        public override void Initialize<_T>(_T instance = null, StateMatchingRoot stateMatchingRoot = null)
        {
            base.Initialize(instance, stateMatchingRoot);
            animator = root.animator;
        }
        [Button]
        void UpdateAnimationEvents()
        {
            groupController.RemoveAllInItems(items => items.value == null || items.value.animationEvent == null);
            UnityEditor.Animations.AnimatorController animatorController;
            animatorController = animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
            #region Method1
            if (animatorController != null)
            {
                foreach (AnimationClip clip in animatorController.animationClips)
                {
                    string clipName = clip.name;
                    AnimationEvent[] animEvents = AnimationUtility.GetAnimationEvents(clip);
                    int i = 0;
                    foreach (AnimationEvent animEvent in animEvents)
                    {
                        string animEventName = "#" + i.ToString() + " Event in: " + clipName;
                        UnityAnimationEvent newAnimationEvent = new UnityAnimationEvent(animEventName, animEvent);
                        groupController.AddItem(animEventName, newAnimationEvent, clipName);
                        i++;
                    }
                }
            }
            #endregion
            #region Method2
            /*foreach (UnityEditor.Animations.AnimatorControllerLayer layer in animatorController.layers)
            {
                foreach(UnityEditor.Animations.ChildAnimatorState childState in layer.stateMachine.states)
                {
                    if(childState.state.motion is AnimationClip animationClip)
                    {
                        AnimationClip clip = animationClip;
                        string clipName = clip.name;
                        AnimationEvent[] animEvents = AnimationUtility.GetAnimationEvents(clip);
                        int i = 0;
                        foreach (AnimationEvent animEvent in animEvents)
                        {
                            string animEventName = "#" + i.ToString() + " Event in: " + clipName;
                            UnityAnimationEvent newAnimationEvent = new UnityAnimationEvent(animEventName,AnimationEvent);
                            groupController.AddItem(animEventName, newAnimationEvent, clipName);
                            i++;
                        }
                    }
                }
            }*/
            #endregion
        }
        public bool autoUpdate = true;
        private void OnDrawGizmos()
        {
            if (autoUpdate && animator != null && groupController != null) UpdateAnimationEvents();
        }
    }

}
