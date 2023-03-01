using System;
using System.Collections.Generic;
using System.Linq;
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

        public static void AddInitializer(ref List<StateMatchingMonoBehaviourInitializer> list, StateMatchingMonoBehaviourInitializer initializer)
        {
            if (list.Find(x => x.pureName == initializer.pureName) == null) list.Add(initializer);
            else list.Find(x => x.pureName == initializer.pureName).Initialize();
        }

        public static void AddGroupInitializer(ref List<ExecuterGroupInitializer> list, ExecuterGroupInitializer initializer)
        {
            if (list.Find(x => x.pureName == initializer.pureName) == null) list.Add(initializer);
            else list.Find(x => x.pureName == initializer.pureName).Initialize();
        }
        public static void AddExecuterInitializer(ref List<ExecuterInitializer> list, ExecuterInitializer initializer)
        {
            if (list.Find(x => x.pureName == initializer.pureName) == null) list.Add(initializer);
            else list.Find(x => x.pureName == initializer.pureName).Initialize();
        }

    }

}
