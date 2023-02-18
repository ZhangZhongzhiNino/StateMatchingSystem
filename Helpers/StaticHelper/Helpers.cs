using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEditor;

namespace StateMatching.Helper
{
    public static class Helpers
    {
        public static GameObject CreateGameObject(string objName, Transform parent)
        {
            GameObject newGameObject = new GameObject();
            if (parent) newGameObject.transform.SetParent(parent);
            ResetLocalTransform(newGameObject);
            newGameObject.name = objName;
            return newGameObject;

        }
        public static void CreateGameObjectWithScript<T>(string objName, Transform parent,out GameObject objReference ,out T scriptReference, T instance = null, StateMatchingRoot root = null) where T: MonoBehaviour, IStateMatchingComponent
        {
            objReference = CreateGameObject(objName, parent);
            scriptReference = (AddStateMatchingComponent<T>(objReference, instance, root));
        }
        public static T AddStateMatchingComponent<T>(GameObject obj,T instance = null, StateMatchingRoot root = null) where T: MonoBehaviour, IStateMatchingComponent
        {
            T newComponent = obj.AddComponent<T>();
            newComponent.Initialize<T>(instance, root);
            return newComponent;
        }
        public static void RemoveGameObject( GameObject obj)
        {
            if (!obj) return;
            List<Transform> childs = new List<Transform>();
            for (int i = 0; i < obj.transform.childCount; i++) childs.Add(obj.transform.GetChild(i));
            if(childs != null)foreach(Transform t in childs)
            {
                GameObject _obj = t.gameObject;
                RemoveGameObject( _obj);
            }
            if (Application.isPlaying) MonoBehaviour.Destroy(obj);
            else MonoBehaviour.DestroyImmediate(obj);
        }
        public static void RemoveComponent( MonoBehaviour component)
        {
            if (Application.isPlaying) MonoBehaviour.Destroy(component);
            else MonoBehaviour.DestroyImmediate(component);
        }
        public static void RemoveAllComponentInGameObject<T>(GameObject obj, Predicate<T> match) where T: MonoBehaviour
        {
            List<T> toRemove = obj.GetComponents<T>().ToList();
            toRemove = toRemove.FindAll(match);
            if (Application.isPlaying)
            {
                foreach(T component in toRemove)
                {
                    MonoBehaviour.Destroy(component);
                }
            }
            else
            {
                foreach (T component in toRemove)
                {
                    MonoBehaviour.DestroyImmediate(component);
                }
            }
        }
        public static void ResetLocalTransform(GameObject obj)
        {
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }
        public static T InitiateExtension<T>(string extensionName, GameObject obj, StateMatchingRoot root) where T: MonoBehaviour,IExtension
        {
            T extension = obj.AddComponent<T>();
            extension.Initiate(extensionName, obj, root);
            return extension;
        }
        public static void SetUpExtensions<T>(ref T extension, string name, GameObject parent, StateMatchingRoot root) where T : MonoBehaviour, IExtension
        {
            if (extension == null) parent.TryGetComponent<T>(out extension);
            if (extension != null) extension.Initiate(name, parent, root);
            else extension = Helpers.InitiateExtension<T>(name, parent, root);
        }
        public static void OpenHierarchy(GameObject obj, bool open)
        {
            EditorApplication.ExecuteMenuItem("Window/Panels/7 Hierarchy");
            var hierarchyWindow = EditorWindow.focusedWindow;
            var expandMethodInfo = hierarchyWindow.GetType().GetMethod("SetExpandedRecursive");
            expandMethodInfo.Invoke(hierarchyWindow, new object[] { obj.GetInstanceID(), open });
            if (open)
            {
                foreach (Transform t in obj.transform)
                {
                    if (t == obj.transform) continue;
                    OpenHierarchy(t.gameObject, false);
                }
            }
        }
        public static void ResetHierachy(GameObject rootObj,GameObject OpenCategory = null)
        {
            StateMatchingRoot root = rootObj.GetComponent<StateMatchingRoot>();
            OpenHierarchy(rootObj, false);
            if (root == null) return;
            OpenHierarchy(rootObj, true);
            OpenHierarchy(root.stateMatchingComponent, true);
            if (OpenCategory != null) OpenHierarchy(OpenCategory, true);
        }
    }
}

