using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.InternalEvent
{
    public class InternalEventController : CategoryController, IExtensionController
    {

        public UnityAnimationEventExtension unityAnimationEventHandler;
        public void InitiateExtensions()
        {
            Helpers.SetUpExtensions<UnityAnimationEventExtension>(ref unityAnimationEventHandler, "Unity Animation Event Handler", this.gameObject, root);

        }
    }
}

