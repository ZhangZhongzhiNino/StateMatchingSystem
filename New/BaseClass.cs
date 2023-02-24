using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.NewStateMatching
{
    public abstract class StateMatchingMonoBehaviour : SerializedMonoBehaviour
    {
        public abstract void Initialize();
        public abstract void Refresh();
        public abstract void Remove();
    } 
    public abstract class DataExecuter: StateMatchingMonoBehaviour
    {
        public ExecuterGroup executerGroup;
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1), PropertyOrder(-9999999999)] protected abstract void ResetHierarchy();
        public override void Initialize()
        {
            InitializeDataControllers();
        }
        protected abstract void InitializeDataControllers();
        public override void Remove()
        {
            PreRemoveDataControllers();
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
        protected abstract void PreRemoveDataControllers();
    }
    public abstract class ExecuterGroup : StateMatchingMonoBehaviour
    {
        public ExecuterCategory executerCategory;
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1), PropertyOrder(-9999999999)] protected abstract void ResetHierarchy();
        public override void Initialize()
        {
            InitializeExecuterInitializers();
        }
        protected abstract void InitializeExecuterInitializers();
        public override void Remove()
        {
            RemoveExecuters();
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
        protected abstract void RemoveExecuters();
    }
    public abstract class ExecuterCategory : StateMatchingMonoBehaviour
    {
        public StateMatchingRoot stateMatchingRoot;
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1), PropertyOrder(-9999999999)] protected abstract void ResetHierarchy();
        public override void Initialize()
        {
            InitializeExecuterGroupInitializers();
        }
        protected abstract void InitializeExecuterGroupInitializers();
        public override void Remove()
        {
            RemoveExecuterGroups();
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
        protected abstract void RemoveExecuterGroups();


    }
    

    public abstract class StateMatchingRoot : StateMatchingMonoBehaviour
    {
        public string StateMatchingName;
        public GameObject objRoot;
        public StateMatchingGlobalReference globalReferences;
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1),PropertyOrder(-9999999999)] protected abstract void ResetHierarchy();
        public override void Initialize()
        {
            InitializeExecuterCategorys();
        }
        protected abstract void InitializeExecuterCategorys();
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
                newInstance.Refresh();
                return newInstance;
            }
            newInstance = GeneralUtility.CreateGameObjectWithStateMatchingMonoBehaviour<T>(objName, this.transform);
            newInstance.stateMatchingRoot = this;
            return newInstance;
        }
    }
    public abstract class StateMatchingInitializer<T> : MonoBehaviour where T:StateMatchingRoot
    {
        public StateMatchingGlobalReference globalReferences;
        public T SMSRoot;
        [Button(size: ButtonSizes.Large), GUIColor(0.4f, 1, 1),PropertyOrder(-9999999999)] protected abstract void ResetHierarchy();
        [Button(size:ButtonSizes.Large,Style = ButtonStyle.Box),GUIColor(0.4f,1,0.4f)]
        void InitializeStateMatching(string StateMatchingName)
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
            }
            else
            {
                SMSRoot.Refresh();
            }
        }
        void GetGlobalReference()
        {
            globalReferences = FindObjectOfType<StateMatchingGlobalReference>();
            if (globalReferences == null) globalReferences = GeneralUtility.CreateGameObjectWithStateMatchingMonoBehaviour<StateMatchingGlobalReference>("____State Matching References___");
        }
    }
    public class StateMatchingGlobalReference : StateMatchingMonoBehaviour
    {
        public List<StateMatchingRoot> references;
        public override void Initialize()
        {
            references = new List<StateMatchingRoot>();
        }
        public override void Refresh()
        {
            if (references == null) references = new List<StateMatchingRoot>();
        }
        public override void Remove()
        {
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
    }
    
    public interface IDataExecuter<DataController, Item, ItemCollection>
        where DataController : DataController<Item, ItemCollection>
        where Item : NewStateMatching.Item
        where ItemCollection : Collection<Item>
    {
        public DataController dataController { get; set; }
        
    }

    [InlineEditor]
    public abstract class StateMatchingMonoBehaviourInitializer<T> : StateMatchingScriptableObject where T: StateMatchingMonoBehaviour
    {
        [HideInInspector] public StateMatchingMonoBehaviour creater;
        [HorizontalGroup("Executer Group", width: 0.6f)] public T toCreate;
        public string addButtonName { get => "Create " + typeof(T).Name; }
        [HorizontalGroup("Executer Group", width: 0.6f),Button(Name = "addButtonName"),GUIColor(0.4f,1,0.4f),ShowIf("@executerGroup == null")] public void Create()
        {
            creater.TryGetComponent<T>(out toCreate);
            if (toCreate != null) return;
            GameObject executerGroupObj = GeneralUtility.CreateGameObject("____" + typeof(T).Name, creater.transform);
            toCreate = GeneralUtility.AddStateMatchingBehaviourToGameObject<T>(executerGroupObj);

        }
        protected abstract void AddCreaterReference();
        public string removeButtonName { get => "Remove " + typeof(T).Name; }
        [HorizontalGroup("Executer Group", width: 0.6f), Button(Name = "removeButtonName"), GUIColor(0.4f, 1, 0.4f), ShowIf("@executerGroup == null")] void RemoveExecuterGroup()
        {
            toCreate.Remove();
        }
        protected override void Initialize()
        {
            toCreate = null;
            toCreate = toCreate.GetComponentInChildren<T>();
        }

    }

    public class ExecuterGroupInitializer<T> : StateMatchingMonoBehaviourInitializer<T> where T : ExecuterGroup
    {
        protected override void AddCreaterReference()
        {
            toCreate.executerCategory = creater as ExecuterCategory;
        }
    }
    public class ExecuterInitializer<T> : StateMatchingMonoBehaviourInitializer<T> where T : DataExecuter
    {
        protected override void AddCreaterReference()
        {
            toCreate.executerGroup = creater as ExecuterGroup;
        }
    }
}

