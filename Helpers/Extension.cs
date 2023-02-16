using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

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
        public T extension;
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
            extension = Helpers.AddStateMatchingComponent<T>(extensionObj, root: root);
        }
        [ShowIfGroup("extension group/Remove", Condition = "@extension != null")]
        [Button(name: "@removeName"),GUIColor(1, 0.4f, 0.4f)]
        public void RemoveExtension()
        {
            extension.PreDestroy();
        }
    }
}
