
using Sirenix.OdinInspector;
using UnityEngine;
namespace Nino.NewStateMatching
{
    public abstract class ExecuterGroup : StateMatchingMonoBehaviour
    {
        [FoldoutGroup("Reference")] public ExecuterCategory executerCategory;
        [FoldoutGroup("Reference")] public AddressData address;
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1), PropertyOrder(-9999999999)] public void ResetHierarchy()
        {
            EditorUtility.OpenHierarchy(executerCategory?.stateMatchingRoot?.objRoot, true);
            EditorUtility.OpenHierarchy(executerCategory?.stateMatchingRoot?.gameObject, true);
            EditorUtility.OpenHierarchy(executerCategory?.gameObject, true);
            EditorUtility.OpenHierarchy(this.gameObject, true);
        }
        public override void Initialize()
        {
            address = gameObject.GetComponent<AddressData>();
            if (address == null)
            {
                address = gameObject.AddComponent<AddressData>();
                address.Initialize();
            }
            address.localAddress = WriteLocalAddress();
            address.script = this;
            InitializeGroupedExecuterInitializers();
        }
        protected abstract string WriteLocalAddress();
        protected abstract void InitializeGroupedExecuterInitializers();
        public override void Remove()
        {
            RemoveExecuters();
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
        protected abstract void RemoveExecuters();
    }
}

