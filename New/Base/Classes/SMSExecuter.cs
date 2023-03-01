
using Sirenix.OdinInspector;
using UnityEngine;
namespace Nino.NewStateMatching
{
    public abstract class SMSExecuter: StateMatchingMonoBehaviour
    {
        [FoldoutGroup("Reference")] public ExecuterGroup executerGroup;
        [FoldoutGroup("Reference"), InlineEditor] public AddressData address;
        [FoldoutGroup("Controller"), TitleGroup("Controller/Data"), ShowIf("@dataController != null")] public DataController dataController;
        [TitleGroup("Controller/Dynamic Data"), ShowIf("@dynamicDataController != null")] public DynamicDataController dynamicDataController;
        [TitleGroup("Controller/Compare"), ShowIf("@compareController != null")] public CompareController compareController;
        [TitleGroup("Controller/Action"), ShowIf("@actionController != null")] public ActionController actionController;
        [TitleGroup("Controller/Event"), ShowIf("@eventController != null")] public EventController eventController;

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
            InitializeInstance();
        }
        protected abstract string WriteLocalAddress();
        protected abstract void InitializeInstance();
        public override void Remove()
        {
            PreRemoveInstance();
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
        protected abstract void PreRemoveInstance();
    }
}

