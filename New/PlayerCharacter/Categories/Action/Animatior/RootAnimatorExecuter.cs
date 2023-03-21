using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System;
namespace Nino.NewStateMatching.Action.Animator
{
    public class RootAnimatorExecuter : SMSExecuter
    {

        [ShowInInspector] public UnityEngine.Animator animator
        {
            get
            {
                StateMatchingRoot root = address.GetRootAddress().script as StateMatchingRoot;
                return root?.objRoot?.GetComponent<UnityEngine.Animator>();
            }
        }
        public override void InitializeAfterCreateAddress()
        {
            UpdateAnimatorStates();

        }

        protected override void InitializeInstance()
        {
            DataUtility.AddAction(dataController, "Update Animator State", (object input) => UpdateAnimatorStates());
            DataUtility.AddAction(dataController, "Play State", PlayState, typeof(PlayStateInput));
            DataUtility.AddAction(dataController, "Fade In State", FadeInState, typeof(FadeInStateInput));
        }

        protected override void PreRemoveInstance()
        {

        }
        [Button]
        void UpdateAnimatorStates()
        {
            dataController.ClearItemsButNotActionAndCompairMethods();

            UnityEditor.Animations.AnimatorController animatorController;
            List<UnityEditor.Animations.AnimatorStateMachine> stateMachines = new List<UnityEditor.Animations.AnimatorStateMachine>();



            animatorController = animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
            foreach (UnityEditor.Animations.AnimatorControllerLayer layer in animatorController.layers)
            {
                stateMachines.Add(layer.stateMachine);
            }
            for (int i = 0; i < stateMachines.Count; i++)
            {
                string LayerName = stateMachines[i].name;
                List<UnityEditor.Animations.ChildAnimatorState> childStates = new List<UnityEditor.Animations.ChildAnimatorState>();
                childStates = stateMachines[i].states.ToList();
                foreach (UnityEditor.Animations.ChildAnimatorState childState in childStates)
                {
                    string stateName = LayerName + ": " + childState.state.name;
                    LabledItem getState = dataController.GetLabledItem(stateName, LayerName);
                    if (getState == null)
                    {
                        UnityEditor.Animations.AnimatorState newState = childState.state;
                        LabledItem newStateReference = new LabledItem(newState, stateName, LayerName);
                        dataController.AddItem(newStateReference);
                    }
                }
            }
        }
        [Button]
        public void PlayState(object _stateName)
        {
            PlayStateInput stateName = _stateName as PlayStateInput;
            Item getStateItem = dataController.GetItem(stateName.stateName);
            UnityEditor.Animations.AnimatorState getState = getStateItem?.value as UnityEditor.Animations.AnimatorState;
            if (getState != null)
            {
                animator.Play(getState.name);
            }
        }
        public void FadeInState(object _input)
        {
            FadeInStateInput input = (FadeInStateInput)_input;
            Item getStateItem = dataController.GetItem(input.stateName);
            UnityEditor.Animations.AnimatorState getState = getStateItem?.value as UnityEditor.Animations.AnimatorState;
            if (getState != null)
            {
                animator.CrossFadeInFixedTime(getState.name, input.fadeInTime);
            }
        }
    }
    public struct FadeInStateInput
    {
        public string stateName;
        public float fadeInTime;
    }
    public class PlayStateInput
    {
        public string stateName;
        public PlayStateInput()
        {
            stateName = "";
        }
    }
}

