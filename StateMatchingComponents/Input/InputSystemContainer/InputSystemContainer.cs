using StateMatching.Helper;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Sirenix.OdinInspector;
namespace StateMatching.Input
{
    public class InputSystemContainer : InputExtensionExecuter
    {
        [ShowInInspector]
        [ListDrawerSettings(OnBeginListElementGUI = "DrawEventNames", DraggableItems = false)]
        public List<UnityEvent> events
        {
            get
            {
                var r = GetAllEvents();
                return r.Item1;
            }
            set { }
        }
        void DrawEventNames(int index)
        {
            if (index >= 0 && index < eventNames.Count && index < events.Count)
            {
                GUI.Label(GUILayoutUtility.GetRect(80, 30), eventNames[index]);
            }
        }
        [ShowInInspector]
        [ListDrawerSettings(DraggableItems = false)]
        public List<string> eventNames
        {
            get
            {
                var r = GetAllEvents();
                return r.Item2;
            }
        }

        public List<GameObject> GetAllChilds()
        {
            List<Transform> childsTransform = this.gameObject.GetComponentsInChildren<Transform>().ToList();
            childsTransform.RemoveAll(item => item == this.transform);
            if (childsTransform == null || childsTransform.Count == 0) return null;
            List<GameObject> childsObj = new List<GameObject>();
            foreach (Transform t in childsTransform) { childsObj.Add(t.gameObject); }
            return childsObj;
        }
        public (List<UnityEvent>,List<string>)GetAllEvents()
        {
            List<GameObject> childsObj = GetAllChilds();
            if (childsObj == null || childsObj.Count == 0) return (null, null);
            List<UnityEvent> eventList = new List<UnityEvent>();
            List<string> nameLlist = new List<string>();
            foreach (GameObject obj in childsObj)
            {
                IEventContainer[] component = obj.GetComponents<IEventContainer>();
                
                List<MonoBehaviour> monos = new List<MonoBehaviour>() ;
                foreach(IEventContainer c in component)
                {
                    monos.Add(c as MonoBehaviour);
                }
                foreach (MonoBehaviour targetScript in monos)
                {
                    var fields = targetScript.GetType().GetFields();
                    foreach (var field in fields)
                    {
                        if (field.FieldType == typeof(UnityEvent))
                        {
                            eventList.Add((UnityEvent)field.GetValue(targetScript));
                            nameLlist.Add(targetScript.GetType().Name + ": " + field.Name);
                        }
                    }
                }
            }
            
            return (eventList,nameLlist);
        }

        public bool Contain(string prefabName)
        {
            List<GameObject> childs = GetAllChilds();
            if (childs == null || childs.Count == 0) return false;
            foreach(GameObject obj in childs)
            {
                if (obj.name == prefabName) return true;
            }
            return false;
        }
    }
}

