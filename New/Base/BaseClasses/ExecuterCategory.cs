
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching
{
    public abstract class ExecuterCategory : StateMatchingMonoBehaviour
    {
        public StateMatchingRoot stateMatchingRoot;
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1), PropertyOrder(-9999999999)] protected void ResetHierarchy()
        {
            EditorUtility.OpenHierarchy(stateMatchingRoot?.objRoot, true);
            EditorUtility.OpenHierarchy(stateMatchingRoot?.gameObject, true);
            EditorUtility.OpenHierarchy(this.gameObject, true);
        }
        public override void Initialize()
        {
            InitializeExecuterGroupInitializers();
            InitializeExecuterInitializers();
        }
        protected abstract void InitializeExecuterGroupInitializers();
        protected abstract void InitializeExecuterInitializers();
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

