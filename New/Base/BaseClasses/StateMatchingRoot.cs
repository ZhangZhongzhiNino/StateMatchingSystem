
using UnityEngine;

using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Nino.NewStateMatching
{
    public abstract class StateMatchingRoot : StateMatchingMonoBehaviour 
    {
        public AddressData address;
        public GameObject objRoot;
        public StateMatchingGlobalReference globalReferences;
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1),PropertyOrder(-9999999999)] public void ResetHierarchy()
        {
            EditorUtility.OpenHierarchy(objRoot, true);
            EditorUtility.OpenHierarchy(this.gameObject, true);
        }
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f), PropertyOrder(-100)] public override void Initialize()
        {
            if (address == null) address = ScriptableObject.CreateInstance<AddressData>();
            InitializeExecuterCategorys();
            ResetHierarchy();
        }
        protected abstract void InitializeExecuterCategorys();
        [FoldoutGroup("Remove State Matching",Order = 100),SerializeField,PropertyOrder(-1)] string remove = "";
        [FoldoutGroup("Remove State Matching"), Button(size: ButtonSizes.Large,Style = ButtonStyle.Box),GUIColor(1,0.4f,0.4f),InfoBox("Type in conform to remove")]
        void RemoveStateMatching()
        {
            if(remove == "conform") Remove();
        }
        public override void Remove()
        {
            RemoveExecuterTypes();
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
        protected abstract void RemoveExecuterTypes();

        protected virtual T CreateCategory<T>(string objName) where T : ExecuterCategory
        {
            T newInstance = GetComponentInChildren<T>();
            if (newInstance != null)
            {
                newInstance.Initialize();
            }
            else
            {
                newInstance = GeneralUtility.CreateGameObjectWithStateMatchingMonoBehaviour<T>(objName, this.transform);
                newInstance.stateMatchingRoot = this;
            }
            
            return newInstance;
        }
    }
}

