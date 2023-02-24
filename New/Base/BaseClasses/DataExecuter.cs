
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching
{
    public abstract class DataExecuter : StateMatchingMonoBehaviour
    {
        public ExecuterCategory executerCategory;
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1), PropertyOrder(-9999999999)]
        protected void ResetHierarchy()
        {
            EditorUtility.OpenHierarchy(executerCategory?.stateMatchingRoot?.objRoot, true);
            EditorUtility.OpenHierarchy(executerCategory?.stateMatchingRoot?.gameObject, true);
            EditorUtility.OpenHierarchy(executerCategory?.gameObject, true);
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

