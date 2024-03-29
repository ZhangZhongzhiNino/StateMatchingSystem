using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEditor;


namespace Nino.StateMatching.Helper
{
    public abstract class Extension<T>:MonoBehaviour,IExtension where T: MonoBehaviour, IStateMatchingComponent
    {
        [HideInInspector] public StateMatchingRoot root;
        [HideInInspector] public GameObject controller;
        [HideInInspector] public string extensionName;
        [ReadOnly] public T executer;
        
        string createName { get { return "Create " + extensionName; } }
        string removeName { get { return "Remove " + extensionName; } }
        
        
        public Extension(string _extensionName, GameObject _controller, StateMatchingRoot _root)
        {
            root = _root;
            controller = _controller;
            extensionName = _extensionName;
        }
        public virtual void Initiate(string _extensionName, GameObject _controller, StateMatchingRoot _root)
        {
            root = _root;
            controller = _controller;
            extensionName = _extensionName;
            if (executer == null) executer = this.gameObject.GetComponentInChildren<T>();
            if (executer != null) executer.Initiate<T>(stateMatchingRoot: root);
        }
        [ShowIfGroup("Create", Condition = "@executer == null")]
        [Button(name:"@createName"),GUIColor(0.4f,1,0.4f)]
        [TitleGroup("Create/Title", GroupName = "@extensionName", Alignment = TitleAlignments.Centered, HorizontalLine = true, BoldTitle = true)]
        public void CreateExtension()
        {
            GameObject extensionObj = GeneralUtility.CreateGameObject("____"+extensionName, controller.transform);
            executer = GeneralUtility.AddStateMatchingComponent<T>(extensionObj, root: root);
            EditorUtility.OpenHierarchy(controller,true);

        }
        [ShowIfGroup("Remove", Condition = "@executer != null")]
        [Button(name: "@removeName"),GUIColor(1, 0.4f, 0.4f)]
        [TitleGroup("Remove/Title", GroupName = "@extensionName", Alignment = TitleAlignments.Centered, HorizontalLine = true, BoldTitle = true)]
        public void RemoveExtension()
        {
            executer.PreDestroy();
        }
    }
}
