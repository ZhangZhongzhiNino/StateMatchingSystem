using Sirenix.OdinInspector;

using System;



namespace Nino.NewStateMatching
{
    [Serializable]
    public abstract class StateMatchingScriptableObject : SerializedScriptableObject
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

