
using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.NewStateMatching
{
    [InlineEditor]
    public abstract class StateMatchingMonoBehaviourInitializer<T> : StateMatchingScriptableObject where T: StateMatchingMonoBehaviour
    {
        [HideInInspector] public StateMatchingMonoBehaviour creater;
        [HideLabel] public T content;
        [HideInInspector] public string contentObjName;
        public string addButtonName { get => "Create " + typeof(T).Name; }
        [PropertyOrder(-1), Button(Name = "@addButtonName",ButtonHeight = 40),GUIColor(0.4f,1,0.4f),ShowIf("@content == null")] public virtual void Create()
        {
            creater.TryGetComponent<T>(out content);
            if (content != null) return;
            GameObject executerGroupObj = GeneralUtility.CreateGameObject(contentObjName, creater.transform);
            content = GeneralUtility.AddStateMatchingBehaviourToGameObject<T>(executerGroupObj);
        }
        public string removeButtonName { get => "Remove " + typeof(T).Name; }
        [PropertyOrder(-1), Button(Name = "@removeButtonName", ButtonHeight = 40), GUIColor(1, 0.4f, 0.4f), ShowIf("@content != null")]public virtual void Remove()
        {
            content.Remove();
        }
        protected override void Initialize()
        {
            contentObjName = WriteBeforeName() + WriteName() + WriteAfterName();
        }
        protected abstract string WriteBeforeName();
        protected abstract string WriteName();
        protected abstract string WriteAfterName();
        public void TryFindContent()
        {
            content = creater.GetComponentInChildren<T>();
        }
        protected override void RunOnEveryEnable()
        {
            TryFindContent();
        }
    }
}

