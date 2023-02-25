using Sirenix.OdinInspector;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using Nino.StateMatching.Data;
using Nino.StateMatching.Action;
using Nino.StateMatching.Helper;
using Nino.StateMatching.Input;
using Nino.StateMatching.StateMachine;
using Nino.StateMatching.Variable;
using Nino.StateMatching.InternalEvent;
using Nino.StateMatching.Helper.New;
namespace Nino.StateMatching
{
    public class StateMatchingRoot : MonoBehaviour
    {
        
        [Button(ButtonSizes.Large), GUIColor(0.4f, 1, 1),PropertyOrder(-99999999999)]
        void ResetHierarchy()
        {
            EditorUtility.ResetHierachy(this.gameObject);
        }

        #region GlobalReference
        [FoldoutGroup("Global Reference")] public StateMatchingGlobalReference stateMatchingGlobalReference;
        [FoldoutGroup("Global Reference")] public string stateMatchingName;
        #endregion
        #region UnityReference
        [FoldoutGroup("Unity Reference")] public UnityEngine.Animator animator;

        [FoldoutGroup("Unity Reference"),Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)]
        public void FindUnityComponents()
        {
            FindUnityComponent(ref animator);
        }
        [FoldoutGroup("Unity Reference"), Button(ButtonSizes.Large), GUIColor(1, 0.4f, 0.4f)]
        void ClearUnityReference()
        {
            animator = null;
        }

        
        #endregion
        #region StateMatchingReference
        [FoldoutGroup("State Matching Reference")] public GameObject componentHolderObj;
        [FoldoutGroup("State Matching Reference")] public StateMatchingComponentRoot componentHolder;

        [FoldoutGroup("State Matching Reference")] [InlineEditor] public RootReferences rootReferences;

        [Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)]
        void SetUpStateMatchingComponent()
        {
            if (!rootReferences) rootReferences = new RootReferences();
            FindUnityComponents();
            SetUpComponentHolder();
            SetUpComponentInComponentHolder();
            ReadComponentRoot();
            SetUpCategories();
            AddFunctionToUpdater();
            WriteComponentRoot();
            EditorUtility.ResetHierachy(this.gameObject);
        }
        void SetUpComponentHolder()
        {
            componentHolder = this.gameObject.GetComponentInChildren<StateMatchingComponentRoot>();
            if (componentHolder == null) CreateComponentHolder();
            else componentHolderObj = componentHolder.gameObject;
            if (componentHolder.rootReferences == null) componentHolder.rootReferences = new RootReferences();
        }
        void CreateComponentHolder()
        {
            componentHolderObj = GeneralUtility.CreateGameObject("_____stateMatchingComponent_____", this.transform);
            componentHolder = componentHolderObj.AddComponent<StateMatchingComponentRoot>();
        }
        void SetUpComponentInComponentHolder()
        {
            componentHolder.rootReferences.actionRoot = componentHolderObj.GetComponent<ActionRoot>();
            componentHolder.rootReferences.editModeUpdater = componentHolderObj.GetComponent<EditModeUpdater>();
            componentHolder.rootReferences.folderPathManager = componentHolder.GetComponent<FolderPathManager>();
            if (componentHolder.rootReferences.actionRoot == null) componentHolder.rootReferences.actionRoot = ActionUtility.CreateActionRoot(this);
            if (componentHolder.rootReferences.editModeUpdater == null) componentHolder.rootReferences.editModeUpdater = componentHolderObj.AddComponent<EditModeUpdater>();
            if (componentHolder.rootReferences.folderPathManager == null) componentHolder.rootReferences.folderPathManager = componentHolderObj.AddComponent<FolderPathManager>();
        }
        void SetUpCategories()
        {
            CreateGameObjectWithScript<InputCategory>("Category: Inputs ____", componentHolderObj.transform, ref rootReferences.inputObj, ref rootReferences.inputCategory);
            CreateGameObjectWithScript<InternalEventCategory>("Category: Internal Event ____", componentHolderObj.transform, ref rootReferences.internalEventObj, ref rootReferences.internalEventCategory);
            CreateGameObjectWithScript<VariableCategory>("Category: Variable ____", componentHolderObj.transform, ref rootReferences.variableObj, ref rootReferences.variableCategory);
            CreateGameObjectWithScript<DataCategory>("Category: Datas ____", componentHolderObj.transform, ref rootReferences.dataObj, ref rootReferences.dataCategory);
            CreateGameObjectWithScript<ActionCategory>("Category: Actions ____", componentHolderObj.transform, ref rootReferences.actionObj, ref rootReferences.actionCategory);
            CreateGameObjectWithScript<StateMachineCategory>("Category: StateMachine ____", componentHolderObj.transform, ref rootReferences.stateMachineObj, ref rootReferences.stateMachineCategory);
        }
        public void ReadComponentRoot()
        {
            rootReferences = componentHolder.rootReferences;
        }
        public void WriteComponentRoot()
        {
            componentHolder.rootReferences = rootReferences;
        }
        public void FindUnityComponent<T>(ref T component) where T : Component
        {
            if (component == null)
            {
                component = GetComponent<T>();
            }
        }
        public void CreateGameObjectWithScript<T>(string objName, Transform parent, ref GameObject objReference, ref T scriptReference) where T : CategoryController
        {
            if (objReference == null) GeneralUtility.CreateGameObjectWithScript<T>(objName, parent, out objReference, out scriptReference, root: this);
            if (scriptReference == null) scriptReference = GeneralUtility.AddStateMatchingComponent<T>(objReference, root: this);
            else scriptReference.Initiate<T>(stateMatchingRoot: this);
        }
        
        [Button(ButtonSizes.Large), GUIColor(1, 0.4f, 0.4f)]
        void ClearAllStateMatchingComponent()
        {
            bool confirm = UnityEditor.EditorUtility.DisplayDialog("Confirmation", "Do you conform to remove everything?", "Yes", "No");
            if (!confirm) return;
            if (rootReferences.dataCategory?.humanoidInfoDataExtension?.executer != null) rootReferences.dataCategory?.humanoidInfoDataExtension?.executer?.PreDestroy();
            GeneralUtility.RemoveGameObject(componentHolderObj);
        }
        
        #endregion


        public void AnimationEventHaldler(AnimationEvent animationEvent)
        {
            string animationClipName = animationEvent.animatorClipInfo.clip.name;
            int currentEventIndex = animationEvent.intParameter;
            if (rootReferences.internalEventCategory.unityAnimationEventExtension.executer == null) return;
            UnityAnimationEventExtensionExecuter animEventHandler = rootReferences.internalEventCategory.unityAnimationEventExtension.executer;
            UnityAnimationEventGroup getEventGroup = animEventHandler.groupController.GetGroup(animationClipName) as UnityAnimationEventGroup;
            UnityAnimationEventItem getEvent = getEventGroup.items[currentEventIndex] as UnityAnimationEventItem;
            getEvent.InvokeUnityEvent();
        }

        #region Updater
        public void RemoveDirtyBodyParts()
        {
            if (rootReferences.dataCategory.humanoidInfoDataExtension.executer != null) return;
            List<BodyPartInfoHolder> parts = this.gameObject.GetComponentsInChildren<BodyPartInfoHolder>().ToList();
            if (parts == null) return;
            for(int i = 0;i<parts.Count; i++)
            {
                GeneralUtility.RemoveComponent(parts[i]);
            }
        }
        private void AddFunctionToUpdater()
        {
            if(!rootReferences.editModeUpdater.Contain((System.Action) RemoveDirtyBodyParts)) rootReferences.editModeUpdater.call += RemoveDirtyBodyParts;
        }
        #endregion

    }

}

