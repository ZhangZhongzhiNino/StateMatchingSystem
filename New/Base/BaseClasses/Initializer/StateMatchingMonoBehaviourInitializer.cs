
using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.NewStateMatching
{
    [InlineEditor]
    public abstract class StateMatchingMonoBehaviourInitializer<T> : StateMatchingScriptableObject where T: StateMatchingMonoBehaviour
    {
        [HideInInspector] public StateMatchingMonoBehaviour creater;
        [HideLabel] public T content;
        public string addButtonName { get => "Create " + typeof(T).Name; }
        [PropertyOrder(-1), Button(Name = "@addButtonName",ButtonHeight = 40),GUIColor(0.4f,1,0.4f),ShowIf("@content == null")] public void Create()
        {
            creater.TryGetComponent<T>(out content);
            if (content != null) return;
            GameObject executerGroupObj = GeneralUtility.CreateGameObject("____" + typeof(T).Name, creater.transform);
            content = GeneralUtility.AddStateMatchingBehaviourToGameObject<T>(executerGroupObj);
            AddCreaterReference();
        }
        protected abstract void AddCreaterReference();
        public string removeButtonName { get => "Remove " + typeof(T).Name; }
        [PropertyOrder(-1), Button(Name = "@removeButtonName", ButtonHeight = 40), GUIColor(1, 0.4f, 0.4f), ShowIf("@content != null")] void RemoveExecuterGroup()
        {
            content.Remove();
        }
        protected override void Initialize()
        {
        }
        public void TryFindContent()
        {
            content = creater.GetComponentInChildren<T>();
        }
    }
}

