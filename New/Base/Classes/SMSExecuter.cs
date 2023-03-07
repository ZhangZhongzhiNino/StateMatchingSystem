﻿
using Sirenix.OdinInspector;
using UnityEngine;
namespace Nino.NewStateMatching
{
    public abstract class SMSExecuter: StateMatchingMonoBehaviour
    {
        [FoldoutGroup("Reference")] public ExecuterGroup executerGroup;
        [FoldoutGroup("Reference"), InlineEditor] public AddressData address;
        [FoldoutGroup("Controller"), TitleGroup("Controller/Data"), ShowIf("@dataController != null")] public DataController dataController;
        [TitleGroup("Controller/Compare"), ShowIf("@compareController != null")] public CompareController compareController;

        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1), PropertyOrder(-9999999999)] public void ResetHierarchy() 
        {
            EditorUtility.OpenHierarchy(executerGroup?.executerCategory?.stateMatchingRoot?.objRoot, true);
            EditorUtility.OpenHierarchy(executerGroup?.executerCategory?.stateMatchingRoot?.gameObject, true);
            EditorUtility.OpenHierarchy(executerGroup?.executerCategory?.gameObject, true);
            EditorUtility.OpenHierarchy(executerGroup?.gameObject, true);
        }
        [Button]
        public override void Initialize()
        {
            if (address == null) address = ScriptableObject.CreateInstance<AddressData>();
            if (dataController == null) dataController = ScriptableObject.CreateInstance<DataController>();
            address.script = this;
            InitializeInstance();
        }
        protected abstract void InitializeInstance();
        public override void Remove()
        {
            PreRemoveInstance();
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
        protected abstract void PreRemoveInstance();
    }
}

