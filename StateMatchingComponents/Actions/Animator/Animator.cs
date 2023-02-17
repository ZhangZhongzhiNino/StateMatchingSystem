using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using StateMatching.Helper;
using System;

namespace StateMatching.Action
{
    public class Animator : GroupExtensionExecuter<UnityStateReference,UnityStateReferenceValue>
    {
        
        public UnityEngine.Animator animator;

        private void OnDrawGizmos()
        {
            if(autoUpdate && animator!=null && groupController != null) UpdateAnimatorStates();
        }

        public override void Initialize<T>(T instance = null, StateMatchingRoot stateMatchingRoot = null) 
        {
            base.Initialize<T>(instance, stateMatchingRoot);
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
                    UnityStateReference getState = groupController.GetItem(i.ToString(), i.ToString() + ":" + stateName);
                    if(getState == null)
                    {
                        UnityStateReference newStateReference = new UnityStateReference(i.ToString() + ":" + stateName,i,childState.state);
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

        
    }
}

