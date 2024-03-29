﻿
using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.NewStateMatching
{
    public abstract class StateMatchingRootInitializer<T> : MonoBehaviour where T:StateMatchingRoot
    {
        [FoldoutGroup("Reference")] public StateMatchingGlobalReference globalReferences;
        [FoldoutGroup("Reference")] public T SMSRoot;
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1),PropertyOrder(-9999999999)] protected void ResetHierarchy()
        {
            EditorUtility.OpenHierarchy(this.gameObject, true);
            if(SMSRoot)EditorUtility.OpenHierarchy(SMSRoot?.gameObject, true);
        }
        [Button(size:ButtonSizes.Large,Style = ButtonStyle.Box),GUIColor(0.4f,1,0.4f),PropertyOrder(-100)]
        void InitializeStateMatching(string rootAddressName)
        {
            GetGlobalReference();
            SMSRoot = GetComponentInChildren<T>();
            if (SMSRoot == null)
            {
                SMSRoot = GeneralUtility.CreateGameObjectWithStateMatchingMonoBehaviour<T>("____" + typeof(T).Name + "____", this.transform);
                SMSRoot.Initialize();
                SMSRoot.objRoot = this.gameObject;
                SMSRoot.globalReferences = globalReferences;
                globalReferences.references.Add(SMSRoot);
                globalReferences.references.RemoveAll(x => x == null);
                SMSRoot.address.localAddress = rootAddressName;
                SMSRoot.address.UpdateGlobalAddressOfSystem();
            }
            else
            {
                SMSRoot.Initialize();
                SMSRoot.address.localAddress = rootAddressName;
                SMSRoot.address.UpdateGlobalAddressOfSystem();
            }
            ResetHierarchy();
        }
        void GetGlobalReference()
        {
            globalReferences = FindObjectOfType<StateMatchingGlobalReference>();
            if (globalReferences == null) globalReferences = GeneralUtility.CreateGameObjectWithStateMatchingMonoBehaviour<StateMatchingGlobalReference>("____State Matching References___");
        }
    }
}

