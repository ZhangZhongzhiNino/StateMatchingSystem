
using Sirenix.OdinInspector;
using UnityEngine;
namespace Nino.NewStateMatching
{
    public abstract class SMSExecuter: StateMatchingMonoBehaviour
    {
        [FoldoutGroup("Reference")] public ExecuterGroup executerGroup;
        [FoldoutGroup("Reference")] public AddressData address;
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
            address.localAddress = WriteLocalAddress();
            address.script = this;
            InitializeDataController();
        }
        protected abstract string WriteLocalAddress();
        protected abstract void InitializeDataController();
        public override void Remove()
        {
            PreRemoveDataControllers();
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
        protected abstract void PreRemoveDataControllers();
    }
}

