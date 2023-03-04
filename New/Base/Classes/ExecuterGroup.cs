
using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;

namespace Nino.NewStateMatching
{
    public abstract class ExecuterGroup : StateMatchingMonoBehaviour
    {
        [FoldoutGroup("Reference")] public ExecuterCategory executerCategory;
        [FoldoutGroup("Reference"), InlineEditor] public AddressData address;
        [TitleGroup("Executer"), ListDrawerSettings(HideAddButton = true, DraggableItems = false, HideRemoveButton = true, ListElementLabelName = "lableName"),LabelWidth(400),ShowIf("@initializers !=null && initializers.Count != 0")] public List<ExecuterInitializer> initializers;
        [TitleGroup("Executer"), ListDrawerSettings(HideAddButton = true, DraggableItems = false, HideRemoveButton = true, ListElementLabelName = "@address.localAddress"), LabelWidth(400), ShowIf("@FSMs !=null && FSMs.Count != 0")] public List<SMS_FSM> FSMs;
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1), PropertyOrder(-9999999999)] public void ResetHierarchy()
        {
            EditorUtility.OpenHierarchy(executerCategory?.stateMatchingRoot?.objRoot, true);
            EditorUtility.OpenHierarchy(executerCategory?.stateMatchingRoot?.gameObject, true);
            EditorUtility.OpenHierarchy(executerCategory?.gameObject, true);
            EditorUtility.OpenHierarchy(this.gameObject, true);
        }
        public override void Initialize()
        {
            if (address == null) address = ScriptableObject.CreateInstance<AddressData>();
            address.localAddress = WriteLocalAddress();
            address.script = this;
            if (initializers == null) initializers = new List<ExecuterInitializer>();
            AddExecuterInitializers();

            executerCategory.address.AddChild(address);
            executerCategory.address.UpdateGlobalAddressInChild();

            if (FSMs == null) FSMs = new List<SMS_FSM>();
        }
        protected abstract string WriteLocalAddress();
        protected abstract void AddExecuterInitializers();
        public override void Remove()
        {
            RemoveExecuters();
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
        protected void RemoveExecuters()
        {
            foreach(ExecuterInitializer initializer in initializers)
            {
                initializer.Remove();
            }
        }

    }
}

