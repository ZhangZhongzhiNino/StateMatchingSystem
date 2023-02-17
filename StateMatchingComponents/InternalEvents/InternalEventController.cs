using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.InternalEvent
{
    public class InternalEventController : CategoryController, IExtensionController
    {

        public UnityAnimationEventHandlerExtension unityAnimationEventHandler;
        public void InitiateExtensions()
        {
            Helpers.SetUpExtensions<UnityAnimationEventHandlerExtension>(ref unityAnimationEventHandler, "Unity Animation Event Handler", this.gameObject, root);

        }
    }
}

