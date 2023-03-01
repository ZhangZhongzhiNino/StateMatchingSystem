using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Trigger.PlayerInput
{
    public class InputExecuter_TouchGesture : SMSExecuter
    {
        public InputReader inputReader;
        public PlayerMoveControl playerMoveControl;
        public SlideGestureInput slideGestureInput;
        protected override void InitializeInstance()
        {
            if (eventController == null) 
            {
                eventController = new EventController();
                eventController.eventInfos.Add(new EventInfo("Start Move"));
                eventController.eventInfos.Add(new EventInfo("Stop Move"));
                eventController.eventInfos.Add(new EventInfo("Start Run"));
                eventController.eventInfos.Add(new EventInfo("Stop Run"));
                eventController.eventInfos.Add(new EventInfo("Tap L"));
                eventController.eventInfos.Add(new EventInfo("Tap R"));
                eventController.eventInfos.Add(new EventInfo("Slide 8"));
                eventController.eventInfos.Add(new EventInfo("Slide 2"));
                eventController.eventInfos.Add(new EventInfo("Slide 4"));
                eventController.eventInfos.Add(new EventInfo("Slide 6"));
                eventController.eventInfos.Add(new EventInfo("Slide 82"));
                eventController.eventInfos.Add(new EventInfo("Slide 28"));
                eventController.eventInfos.Add(new EventInfo("Slide 46"));
                eventController.eventInfos.Add(new EventInfo("Slide 64"));
            }
            if(dynamicDataController == null)
            {
                dynamicDataController = new DynamicDataController();
                dynamicDataController.dynamicDatas.Add(new DynamicInputData_Vector2("Move Direction"));

            }
            if (inputReader == null) inputReader = gameObject.AddComponent<InputReader>();
            if (playerMoveControl == null) playerMoveControl = gameObject.AddComponent<PlayerMoveControl>();
            if (slideGestureInput == null) slideGestureInput = gameObject.AddComponent<SlideGestureInput>();
            playerMoveControl.executer = this;
            slideGestureInput.executer = this;
        }

        protected override void PreRemoveInstance()
        {
            
        }

        protected override string WriteLocalAddress()
        {
            return "Player Input";
        }
    }

}
