
using Sirenix.OdinInspector;
using UnityEngine;
namespace Nino.NewStateMatching
{
    public abstract class SMSExecuter: StateMatchingMonoBehaviour
    {
        [FoldoutGroup("Reference")] public ExecuterGroup executerGroup;
        [FoldoutGroup("Reference"), InlineEditor] public AddressData address;
        [ShowIf("@dataController != null")] public DataController dataController;
        
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1), PropertyOrder(-9999999999)] public void ResetHierarchy() 
        {
            EditorUtility.OpenHierarchy(executerGroup?.executerCategory?.stateMatchingRoot?.objRoot, true);
            EditorUtility.OpenHierarchy(executerGroup?.executerCategory?.stateMatchingRoot?.gameObject, true);
            EditorUtility.OpenHierarchy(executerGroup?.executerCategory?.gameObject, true);
            EditorUtility.OpenHierarchy(executerGroup?.gameObject, true);
        }
        public override void Initialize()
        {
            if (address == null) address = ScriptableObject.CreateInstance<AddressData>();
            if (dataController == null) dataController = ScriptableObject.CreateInstance<DataController>();
            address.script = this;
            InitializeInstance();
        }
        protected abstract void InitializeInstance();
        public abstract void InitializeAfterCreateAddress();
        public override void Remove()
        {
            PreRemoveInstance();
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
        protected abstract void PreRemoveInstance();
    }
}

