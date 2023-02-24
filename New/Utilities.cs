using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace Nino.NewStateMatching
{

    public static class GeneralUtility
    {

        public static GameObject CreateGameObject(string objName, Transform parent = null)
        {
            GameObject newGameObject = new GameObject();
            if (parent) newGameObject.transform.SetParent(parent);
            ResetLocalTransform(newGameObject);
            newGameObject.name = objName;
            return newGameObject;

        }
        public static void ResetLocalTransform(GameObject obj)
        {
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }
        public static void RemoveGameObject(GameObject obj)
        {
            if (!obj) return;
            if (Application.isPlaying) MonoBehaviour.Destroy(obj);
            else MonoBehaviour.DestroyImmediate(obj);
        }
        public static void RemoveGameObjectWithChild(GameObject obj)
        {
            if (!obj) return;
            List<Transform> childs = new List<Transform>();
            for (int i = 0; i < obj.transform.childCount; i++) childs.Add(obj.transform.GetChild(i));
            if (childs != null) foreach (Transform t in childs)
                {
                    GameObject _obj = t.gameObject;
                    RemoveGameObjectWithChild(_obj);
                }
            RemoveGameObject(obj);
        }
        public static void RemoveComponent(MonoBehaviour component)
        {
            if (!component) return;
            if (Application.isPlaying) MonoBehaviour.Destroy(component);
            else MonoBehaviour.DestroyImmediate(component);
        }
        public static void RemoveAllComponentInGameObject<T>(GameObject obj, Predicate<T> match) where T : MonoBehaviour
        {
            List<T> toRemove = obj.GetComponents<T>().ToList();
            toRemove = toRemove.FindAll(match);
            if (Application.isPlaying)
            {
                foreach (T component in toRemove)
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
        public static T AddStateMatchingBehaviourToGameObject<T>(GameObject obj) 
            where T: StateMatchingMonoBehaviour
        {
            T instance = obj.AddComponent<T>();
            instance.Initialize();
            return instance;
        }
        public static T CreateGameObjectWithStateMatchingMonoBehaviour<T>(string objName, Transform parent = null) where T: StateMatchingMonoBehaviour
        {
            GameObject newObj = CreateGameObject(objName, parent);
            T newScript = newObj.AddComponent<T>();
            newScript.Initialize();
            return newScript;
        }
        public static Initializer InitializeStateMatchingMonoBehaviourInitializer<Initializer,ToCreate>(StateMatchingMonoBehaviour Creater) 
            where Initializer : StateMatchingMonoBehaviourInitializer<ToCreate>
            where ToCreate : StateMatchingMonoBehaviour
        {
            Initializer newStateMatchingMonoBehaviourInitializer = ScriptableObject.CreateInstance<Initializer>();
            newStateMatchingMonoBehaviourInitializer.creater = Creater;
            return newStateMatchingMonoBehaviourInitializer;
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
        public static void ResetHierarchy(GameObject root, GameObject category = null)
        {
            
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
        public static T GetItemInList<T>(Predicate<T> match, List<T> list) where T : ScriptableObject
        {
            if (ListIsNullOrEmpty<T>(list)) return null;
            T getItem = list.Find(match);
            if (ScriptableObjectIsDefaultOrNull<T>(getItem)) return null;
            return getItem;
        }
        public static List<T> GetItemsInList<T>(Predicate<T> match, List<T> list) where T : ScriptableObject
        {
            if (DataUtility.ListIsNullOrEmpty<T>(list)) return null;
            List<T> r = list.FindAll(match);
            if (DataUtility.ListIsNullOrEmpty<T>(r)) return null;
            return r;
        }
        public static bool AddItemToList<T>(T newItem, List<T> list) where T : Item
        {
            if (ListContainItem<T>(item => item.itemName == newItem.itemName, list)) return false;
            list.Add(newItem);
            return true;
        }
        public static T AddItemToList<T>(string newItemName, List<T> list) where T : Item
        {
            if (ListContainItem(item => item.itemName == newItemName, list)) return GetItemInList(item => item.itemName == newItemName, list);
            T newItem = ScriptableObject.CreateInstance<T>();
            newItem.itemName = newItemName;
            if (AddItemToList(newItem, list)) return newItem;
            else throw new Exception("Unknow error in create new Item");
        }
        public static int RemoveItemsInList<T>(Predicate<T> match, List<T> list) where T : ScriptableObject
        {
            int r = list.FindAll(match).Count;
            list.RemoveAll(match);
            return r;
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
        public static T CopyScriptableObject<T>(T SO) where T : ScriptableObject
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
            if (!Directory.Exists(path + "/" + folder)) Directory.CreateDirectory(path + "/" + folder);
            return Directory.Exists(path + "/" + folder);
        }
    }
    public static class OdinUtility
    {
        public static IEnumerable<ValueDropdownItem<string>> ValueDropDownListSelector(List<string> list, List<string> selectList)
        {
            if (list == null) return new List<ValueDropdownItem<string>>();
            var items = new List<ValueDropdownItem<string>>();
            for (int i = 0; i < list.Count; i++)
            {
                if (selectList.Contains(list[i])) continue;
                items.Add(new ValueDropdownItem<string>(list[i], list[i]));
            }
            return items;
        }
    }

}