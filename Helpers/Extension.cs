using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEditor;


namespace StateMatching.Helper
{
    public class Extension<T>:MonoBehaviour,IExtension where T: MonoBehaviour, IStateMatchingComponent
    {
        [HideInInspector]public StateMatchingRoot root;
        GameObject controller;
        string extensionName;
        string createName { get { return "Create " + extensionName; } }
        string removeName { get { return "Remove " + extensionName; } }
        [BoxGroup("extension group",GroupName = "@extensionName")]
        public T executer;
        public Extension(string _extensionName, GameObject _controller, StateMatchingRoot _root)
        {
            root = _root;
            controller = _controller;
            extensionName = _extensionName;
        }
        public void Initiate(string _extensionName, GameObject _controller, StateMatchingRoot _root)
        {
            root = _root;
            controller = _controller;
            extensionName = _extensionName;
        }
        [ShowIfGroup("extension group/Create", Condition = "@extension == null")]
        [Button(name:"@createName"),GUIColor(0.4f,1,0.4f)]
        public void CreateExtension()
        {
            GameObject extensionObj = Helpers.CreateGameObject("____"+extensionName, controller.transform);
            executer = Helpers.AddStateMatchingComponent<T>(extensionObj, root: root);
            Helpers.OpenHierarchy(controller,true);
        }
        
        

        [ShowIfGroup("extension group/Remove", Condition = "@extension != null")]
        [Button(name: "@removeName"),GUIColor(1, 0.4f, 0.4f)]
        public void RemoveExtension()
        {
            executer.PreDestroy();
        }
    }
}
