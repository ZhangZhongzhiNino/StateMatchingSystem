using Sirenix.OdinInspector;

using System;
using UnityEngine;


namespace Nino.NewStateMatching
{ 
    public abstract class StateMatchingScriptableObject : SerializedScriptableObject
    {
        [HideInInlineEditors,SerializeReference] public bool initialized = false;
        private void OnEnable()
        {
            if (initialized)
            {
                RunOnEveryEnable();
                return;
            }
            initialized = true;
            Initialize();
        }
        protected abstract void Initialize();
        protected abstract void RunOnEveryEnable();
    }
}

