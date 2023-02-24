
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching
{
    public abstract class GroupedDataExecuter: StateMatchingMonoBehaviour
    {
        public ExecuterGroup executerGroup;
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1), PropertyOrder(-9999999999)] protected void ResetHierarchy() 
        {
            EditorUtility.OpenHierarchy(executerGroup?.executerCategory?.stateMatchingRoot?.objRoot, true);
            EditorUtility.OpenHierarchy(executerGroup?.executerCategory?.stateMatchingRoot?.gameObject, true);
            EditorUtility.OpenHierarchy(executerGroup?.executerCategory?.gameObject, true);
            EditorUtility.OpenHierarchy(executerGroup?.gameObject, true);
        }
        public override void Initialize()
        {
            InitializeDataController();
        }
        protected abstract void InitializeDataController();
        public override void Remove()
        {
            PreRemoveDataControllers();
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
        protected abstract void PreRemoveDataControllers();
    }
}

