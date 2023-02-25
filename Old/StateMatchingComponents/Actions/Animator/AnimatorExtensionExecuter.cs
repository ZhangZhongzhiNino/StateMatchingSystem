using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Nino.StateMatching.Helper;
using System;

namespace Nino.StateMatching.Action
{
    public class AnimatorExtensionExecuter : ActionGroupExtensionExecuter<UnityStateReferenceValue>
    {
        
        public UnityEngine.Animator animator;



        public override void Initiate<T>(T instance = null, StateMatchingRoot stateMatchingRoot = null) 
        {
            base.Initiate<T>(instance, stateMatchingRoot);
            animator = root.animator;
            UpdateAnimatorStates();
        }

        public bool autoUpdate = true;
        void UpdateAnimatorStates()
        {
            
            groupController.RemoveAllInItems(items =>items.value==null|| items.value.state == null);

            UnityEditor.Animations.AnimatorController animatorController;
            List<UnityEditor.Animations.AnimatorStateMachine> stateMachines = new List<UnityEditor.Animations.AnimatorStateMachine>();
            
            

            animatorController = animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
            foreach(UnityEditor.Animations.AnimatorControllerLayer layer in animatorController.layers)
            {
                stateMachines.Add(layer.stateMachine);
            }
            for(int i = 0; i < stateMachines.Count; i++)
            {
                List<UnityEditor.Animations.ChildAnimatorState> childStates = new List<UnityEditor.Animations.ChildAnimatorState>();
                childStates = stateMachines[i].states.ToList();
                foreach (UnityEditor.Animations.ChildAnimatorState childState in childStates)
                {
                    string stateName = childState.state.name;
                    UnityStateReferenceItem getState = groupController.GetItem(i.ToString(), i.ToString() + ":" + stateName) as UnityStateReferenceItem;
                    if(getState == null)
                    {
                        UnityStateReferenceItem newStateReference = new UnityStateReferenceItem(i.ToString() + ":" + stateName,i,childState.state);
                        groupController.AddItem(i.ToString()+":"+stateName, newStateReference, i.ToString());
                    }
                }
            }
        }

        public override Type GetGroupControllerType()
        {
            return typeof(UnityStateReferenceGroupController);
        }
        public override Type GetGroupPreviewType()
        {
            return typeof(UnityStateReferenceGroupPreview);
        }
        public override string GetActionGroupName()
        {
            return "Animator";
        }

        public override void InitiateActions()
        {

        }
        public override void EditModeUpdateCalls()
        {
            if (autoUpdate && animator != null && groupController != null) UpdateAnimatorStates();
        }
    }
}

