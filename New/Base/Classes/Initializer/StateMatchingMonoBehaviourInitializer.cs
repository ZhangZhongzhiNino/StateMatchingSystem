﻿
using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.NewStateMatching
{
    public abstract class StateMatchingMonoBehaviourInitializer  
    {
        public StateMatchingMonoBehaviourInitializer(StateMatchingMonoBehaviour creater, string name)
        {
            this.creater = creater;
            this.pureName = name;
            this.contentObjName = WriteBeforeName() + name + WriteAfterName();
        }

        [HideInInspector] public StateMatchingMonoBehaviour creater;
        [HideInInspector] public StateMatchingMonoBehaviour content;
        [HideInInspector] public string pureName;
        [HideInInspector, SerializeField] string contentObjName;
        [HideInInspector] public string lableName 
        { get
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
            if (content == null) content = AddComponentToGameObject(contentObj);
            AssignContentParent();
            AssignContentParent();
            content.Initialize();
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
        protected abstract StateMatchingMonoBehaviour AddComponentToGameObject(GameObject contentObj);
        protected abstract void AssignContentParent();
        protected abstract void UpdateCreaterAddress();
        protected abstract void ResetHierarchy();
        protected abstract void RemoveNullInCreaterAddress();

    }
}

