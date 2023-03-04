
using UnityEngine;

using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Nino.NewStateMatching
{
    public abstract class ExecuterCategory : StateMatchingMonoBehaviour
    {
        [FoldoutGroup("Reference")] public StateMatchingRoot stateMatchingRoot;
        [FoldoutGroup("Reference"), InlineEditor] public AddressData address;
        [TitleGroup("Executer Group"),ListDrawerSettings(HideAddButton = true,DraggableItems =false,HideRemoveButton = true,ListElementLabelName = "lableName"),LabelWidth(400)] public List<ExecuterGroupInitializer> initializers;
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
            if (initializers == null) initializers = new List<ExecuterGroupInitializer>();
            AddExecuterGroupInitializers();
            
        }
        protected abstract string WriteAddress();
        protected abstract void AddExecuterGroupInitializers();
        public override void Remove()
        {
            RemoveExecuterGroups();
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
        protected void RemoveExecuterGroups()
        {
            foreach (ExecuterGroupInitializer initializer in initializers)
            {
                initializer.executerGroup.Remove();
            }
        }


       

    }
}

