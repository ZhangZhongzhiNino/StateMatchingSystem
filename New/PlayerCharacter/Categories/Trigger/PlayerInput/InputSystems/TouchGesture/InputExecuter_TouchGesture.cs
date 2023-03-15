using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace Nino.NewStateMatching.PlayerCharacter.Trigger.PlayerInput
{
    public class InputExecuter_TouchGesture : SMSExecuter
    {
        public InputReader inputReader;
        public PlayerMoveControl playerMoveControl;
        public SlideGestureInput slideGestureInput;

        public override void InitializeAfterCreateAddress()
        {
            
        }

        protected override void InitializeInstance()
        {
            dataController.AddLabledItem("Start Move", "Trigger", typeof(UnityEvent),false);
            dataController.AddLabledItem("Stop Move", "Trigger", typeof(UnityEvent), false);
            dataController.AddLabledItem("Start Run", "Trigger", typeof(UnityEvent), false);
            dataController.AddLabledItem("Stop Run", "Trigger", typeof(UnityEvent), false);
            dataController.AddLabledItem("Tap L", "Trigger", typeof(UnityEvent), false);
            dataController.AddLabledItem("Tap R", "Trigger", typeof(UnityEvent), false);
            dataController.AddLabledItem("Slide 8", "Trigger", typeof(UnityEvent), false);
            dataController.AddLabledItem("Slide 2", "Trigger", typeof(UnityEvent), false);
            dataController.AddLabledItem("Slide 4", "Trigger", typeof(UnityEvent), false);
            dataController.AddLabledItem("Slide 6", "Trigger", typeof(UnityEvent), false);
            dataController.AddLabledItem("Slide 82", "Trigger", typeof(UnityEvent), false);
            dataController.AddLabledItem("Slide 28", "Trigger", typeof(UnityEvent), false);
            dataController.AddLabledItem("Slide 46", "Trigger", typeof(UnityEvent), false);
            dataController.AddLabledItem("Slide 64", "Trigger", typeof(UnityEvent), false); 

            dataController.AddLabledItem("Move Direction", typeof(Vector2));

            if (inputReader == null) inputReader = gameObject.AddComponent<InputReader>();
            if (playerMoveControl == null) playerMoveControl = gameObject.AddComponent<PlayerMoveControl>();
            if (slideGestureInput == null) slideGestureInput = gameObject.AddComponent<SlideGestureInput>();
            playerMoveControl.executer = this;
            slideGestureInput.executer = this;
        }

        protected override void PreRemoveInstance()
        {
            
        }
        private void OnEnable()
        {
            UnityEvent getEvent = dataController.GetItem("Slide 8").value as UnityEvent;
            getEvent.AddListener(dbug);
        }
        private void OnDisable()
        {
            UnityEvent getEvent = dataController.GetItem("Slide 8").value as UnityEvent;
            getEvent.RemoveListener(dbug);
        }
        public void dbug()
        {
            Debug.Log("8");
        }
    }

}
