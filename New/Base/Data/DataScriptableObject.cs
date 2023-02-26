using Sirenix.OdinInspector.Editor;
using System;


namespace Nino.NewStateMatching
{

    [Serializable]
    public abstract class DataScriptableObject : StateMatchingScriptableObject
    {
        protected override void Initialize()
        {
            InitializeBaseScriptableObject();
            InitializeInstance();
        }
        protected abstract void InitializeBaseScriptableObject();
        protected abstract void InitializeInstance();

    }
}

