using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;
using UnityEditor;

using Nino.StateMatching.Helper.Data;

namespace Nino.StateMatching.Helper
{
    public static class GeneralUtility
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
            newComponent.Initiate<T>(instance, root);
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
            else extension = GeneralUtility.InitiateExtension<T>(name, parent, root);
        }
        
    }
    public static class EditorUtility
    {
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
        public static void ResetHierachy(GameObject rootObj, GameObject OpenCategory = null)
        {
            StateMatchingRoot root = rootObj.GetComponent<StateMatchingRoot>();
            OpenHierarchy(rootObj, false);
            if (root == null) return;
            OpenHierarchy(rootObj, true);
            OpenHierarchy(root.componentHolderObj, true);
            if (OpenCategory != null) OpenHierarchy(OpenCategory, true);
        }

    }
    public static class ActionUtility
    {
        public static T CreateAction<T>(string newActionName, ExtensionExecuter executer,ActionGroup actionGroup) where T:Action
        {
            actionGroup?.actions?.RemoveAll(item => item == null);
            foreach(Action action in actionGroup.actions)
            {
                if (action.name == newActionName) return action as T;
            }
            T newAction = executer.gameObject.AddComponent<T>();
            newAction.Initiate(newActionName, executer);
            actionGroup.actions.Add(newAction);
            return newAction;
        }
        public static ActionGroup CreateActionGroup(string newGroupName,GameObject addToObj, ActionType actionType)
        {
            actionType?.groups?.RemoveAll(item => item == null);
            foreach(ActionGroup group in actionType.groups)
            {
                if (group.groupName == newGroupName) return group;
            }
            ActionGroup newActionGroup = addToObj.AddComponent<ActionGroup>();
            newActionGroup.Initiate(newGroupName);
            actionType.groups.Add(newActionGroup);
            return newActionGroup;
        }
        public static ActionType CreateActionType(string newTypeName, GameObject addToObj, ActionRoot actionRoot)
        {
            actionRoot?.types?.RemoveAll(item => item == null);
            foreach(ActionType type in actionRoot.types)
            {
                if (type.typeName == newTypeName) return type;
            }
            ActionType newActionType = addToObj.AddComponent<ActionType>();
            newActionType.Initiate(newTypeName);
            actionRoot.types.Add(newActionType);
            return newActionType;
        }
        public static ActionRoot CreateActionRoot(StateMatchingRoot root)
        {
            StateMatchingComponentRoot componentRoot = root.componentHolder;
            if (componentRoot.rootReferences.actionRoot != null) return componentRoot.rootReferences.actionRoot;
            ActionRoot newActionRoot = componentRoot.gameObject.GetComponent<ActionRoot>();
            if (newActionRoot != null) return newActionRoot;
            newActionRoot = componentRoot.gameObject.AddComponent<ActionRoot>();
            newActionRoot.Initiate(root.stateMatchingName);
            return newActionRoot;
        }
    }
    public static class DataUtility
    {
        public static bool ScriptableObjectIsDefaultOrNull<T>(T obj) where T : ScriptableObject 
        {
            return (obj == null || obj == default(T));
        }
        public static bool ListIsNullOrEmpty<T>(List<T> list)
        {
            return (list == null || list.Count == 0);
        }
        public static bool ListContainItem<T>(Predicate<T> match, List<T> list) where T : ScriptableObject
        {
            T getItem = GetItemInList<T>(match, list);
            return getItem != null;
        }
        public static T GetItemInList<T>(Predicate<T>match,List<T> list) where T: ScriptableObject
        {
            if (ListIsNullOrEmpty<T>(list)) return null;
            T getItem = list.Find(match);
            if (ScriptableObjectIsDefaultOrNull<T>(getItem)) return null;
            return getItem;
        } 
        public static List<T> GetItemsInList<T>(Predicate<T> match, List<T> list) where T:ScriptableObject
        {
            if (DataUtility.ListIsNullOrEmpty<T>(list)) return null;
            List<T> r = list.FindAll(match);
            if (DataUtility.ListIsNullOrEmpty<T>(r)) return null;
            return r;
        }
        public static bool AddItemToList<T>(T newItem, List<T> list) where T : Data.Item
        {
            if (ListContainItem<T>(item => item.itemName == newItem.itemName, list)) return false;
            list.Add(newItem);
            return true;
        }
        public static T AddItemToList<T>(string newItemName,List<T>list)where T : Data.Item
        {
            if (ListContainItem(item => item.itemName == newItemName,list)) return GetItemInList(item => item.itemName == newItemName,list);
            T newItem = ScriptableObject.CreateInstance<T>();
            newItem.itemName = newItemName;
            if (AddItemToList(newItem,list)) return newItem;
            else throw new Exception("Unknow error in create new Item");
        }
        public static int RemoveItemsInList<T>(Predicate<T> match, List<T> list) where T : ScriptableObject
        {
            int r = list.FindAll(match).Count;
            list.RemoveAll(match);
            return r;
        }
        public static bool AddVarToList<T>(T v ,List<T> list)
        {
            if (list.Contains(v)) return false;
            list.Add(v);
            return true;
        }
        public static List<string> RemoveAllRedundantStringInList(List<string> list)
        {
            list.RemoveAll(x => string.IsNullOrEmpty(x));
            list.RemoveAll(x => string.IsNullOrWhiteSpace(x));
            List<string> newList = new List<string>();
            foreach (string tag in list)
            {
                if (!newList.Contains(tag)) newList.Add(tag);
            }
            return newList;
        }
        public static T CopyScriptableObject<T>(T SO) where T:ScriptableObject
        {
            T r = ScriptableObject.CreateInstance<T>();
            string json = JsonUtility.ToJson(SO);
            JsonUtility.FromJsonOverwrite(json, r);
            return r;
        }
    }
    public static class AssetUtility
    {
        public static bool SaveAsset(UnityEngine.Object obj, string path)
        {
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path);

            if (!AssetDatabase.Contains(obj))
            {
                AssetDatabase.CreateAsset(obj, assetPathAndName);
                return true;
            }
            else
            {
                AssetDatabase.SaveAssetIfDirty(obj);
                return false;
            }
        }
        public static bool CreateFolder(string path, string folder)
        {
            Debug.Log(path);
            if (!Directory.Exists(path)) return false;
            if(!Directory.Exists(path+"/"+folder)) Directory.CreateDirectory(path + "/" + folder);
            return Directory.Exists(path + "/" + folder);
        }
    }
}

