
using UnityEngine;

using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Nino.NewStateMatching
{
    public abstract class ExecuterCategory : StateMatchingMonoBehaviour
    {
        [FoldoutGroup("Reference")] public StateMatchingRoot stateMatchingRoot;
        [FoldoutGroup("Reference"), InlineEditor] public AddressData address;
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1), PropertyOrder(-9999999999)] public void ResetHierarchy()
        {
            EditorUtility.OpenHierarchy(stateMatchingRoot?.objRoot, true);
            EditorUtility.OpenHierarchy(stateMatchingRoot?.gameObject, true);
            EditorUtility.OpenHierarchy(this.gameObject, true);
        }
        public override void Initialize()
        {
            if (address == null) address = ScriptableObject.CreateInstance<AddressData>();
            address.script = this;
            if(string.IsNullOrWhiteSpace(address.localAddress))address.localAddress = WriteAddress();
            InitializeExecuterGroupInitializers();
            
        }
        protected abstract string WriteAddress();
        protected abstract void InitializeExecuterGroupInitializers();
        public override void Remove()
        {
            RemoveExecuterGroups();
            RemoveExecuters();
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
        protected abstract void RemoveExecuterGroups();
        protected abstract void RemoveExecuters();

    }
}

