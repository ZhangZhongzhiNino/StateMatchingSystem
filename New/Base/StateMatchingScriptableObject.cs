using Sirenix.OdinInspector;

using System;


using UnityEngine;
namespace Nino.NewStateMatching
{
    [Serializable]
    public abstract class StateMatchingScriptableObject : SerializedScriptableObject
    {
        public bool initialized = false;
        private void OnEnable()
        {
            if (initialized) return;
            initialized = true;
            Initialize();
        }
        protected abstract void Initialize();

    }
    [Serializable]
    public abstract class StateMatchingScriptableObject_NS : ScriptableObject
    {
        bool initialized = false;
        private void OnEnable()
        {
            if (initialized) return;
            initialized = true;
            Initialize();
        }
        protected abstract void Initialize();

    }
}

