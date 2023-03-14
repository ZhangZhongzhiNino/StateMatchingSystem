
using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.NewStateMatching
{
    public abstract class SMSMonoBehaviourInitializer  
    {
        public SMSMonoBehaviourInitializer(StateMatchingMonoBehaviour creater, string name,System.Type contentType)
        {
            this.creater = creater;
            this.pureName = name;
            this.contentObjName = WriteBeforeName() + name + WriteAfterName();
            this.contentType = contentType;
        }
        [HideInInspector] public System.Type contentType;
        [HideInInspector] public StateMatchingMonoBehaviour creater;
        public StateMatchingMonoBehaviour content;
        [HideInInspector] public string pureName;
        [HideInInspector, SerializeField] string contentObjName;
        [HideInInspector] public string lableName 
        { 
            get
            {
                if (content == null) return pureName;
                else return "_____" + pureName;
            } 
        }
        public string addButtonName { get => "Create " + contentObjName; }
        [PropertyOrder(-1), Button(Name = "@addButtonName",ButtonHeight = 40),GUIColor(0.4f,1,0.4f),ShowIf("@content == null")] public virtual void Create()
        {
            GameObject contentObj = creater.transform.Find(contentObjName)?.gameObject;
            if(contentObj == null) contentObj = GeneralUtility.CreateGameObject(contentObjName, creater.transform);
            if (content == null) content = TryFindContent();
            if (content == null) content = (StateMatchingMonoBehaviour)contentObj.AddComponent(contentType);

            AssignContentParent();
            content.Initialize();
            assignAddress();
            ResetHierarchy();
        }
        public string removeButtonName { get => "Remove " + contentObjName; }
        [PropertyOrder(-1), Button(Name = "@removeButtonName", ButtonHeight = 40), GUIColor(1, 0.4f, 0.4f), ShowIf("@content != null")]public virtual void Remove()
        {
            content.Remove();
            RemoveNullInCreaterAddress();
        }
        public void Initialize()
        {
            if (content == null) content = TryFindContent();
        }
        protected abstract string WriteBeforeName();
        protected abstract string WriteAfterName();
        protected abstract StateMatchingMonoBehaviour TryFindContent();
        protected abstract void AssignContentParent();
        protected abstract void UpdateCreaterAddress();
        protected abstract void ResetHierarchy();
        protected abstract void RemoveNullInCreaterAddress();
        protected abstract void assignAddress();
        
    }
}

