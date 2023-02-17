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
            unityAnimationEventHandler = Helpers.InitiateExtension<UnityAnimationEventHandlerExtension>("Unity Animation Event Handler", this.gameObject, root);

        }
    }
}

