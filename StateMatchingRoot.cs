using Sirenix.OdinInspector;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using Nino.StateMatching.Data;
using Nino.StateMatching.Action;
using Nino.StateMatching.Helper;
using Nino.StateMatching.Input;
using Nino.StateMatching.StateMachine;
using Nino.StateMatching.Variable;
using Nino.StateMatching.InternalEvent;
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
        [FoldoutGroup("Unity Reference")] public CharacterController characterController;
        [FoldoutGroup("Unity Reference")] public Rigidbody rigidBody;

        [FoldoutGroup("Unity Reference"),Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)]
        public void FindUnityComponents()
        {
            FindUnityComponent(ref animator);
            FindUnityComponent<CharacterController>(ref characterController);
            FindUnityComponent<Rigidbody>(ref rigidBody);
        }
        [FoldoutGroup("Unity Reference"), Button(ButtonSizes.Large), GUIColor(1, 0.4f, 0.4f)]
        void ClearUnityReference()
        {
            animator = null;
            characterController = null;
            rigidBody = null;
        }

        
        #endregion
        #region StateMatchingReference
        [FoldoutGroup("State Matching Reference")] 
        [TitleGroup("State Matching Reference/Game Object")]public GameObject stateMatchingComponent;
        [TitleGroup("State Matching Reference/Game Object")] public GameObject inputObj;
        [TitleGroup("State Matching Reference/Game Object")] public GameObject internalEventObj;
        [TitleGroup("State Matching Reference/Game Object")] public GameObject variableObj; 
        [TitleGroup("State Matching Reference/Game Object")] public GameObject dataObj;
        [TitleGroup("State Matching Reference/Game Object")] public GameObject actionObj;
        [TitleGroup("State Matching Reference/Game Object")] public GameObject stateMachineObj;
        [TitleGroup("State Matching Reference/Script Reference")] public InputCategory inputCategory;
        [TitleGroup("State Matching Reference/Script Reference")] public InternalEventCategory internalEventCategory;
        [TitleGroup("State Matching Reference/Script Reference")] public VariableCategory variableCategory;
        [TitleGroup("State Matching Reference/Script Reference")] public DataCategory dataCategory;
        [TitleGroup("State Matching Reference/Script Reference")] public ActionCategory actionCategory;
        [TitleGroup("State Matching Reference/Script Reference")] public StateMachineCategory stateMachineCategory;
        [TitleGroup("State Matching Reference/Action Reference")] public ActionRoot actionRoot;
        [FoldoutGroup("State Matching Reference"),Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)]
        void SetUpStateMatchingComponent()
        {
            actionRoot = ActionUtility.CreateActionRoot(this);
            if(stateMatchingComponent == null) stateMatchingComponent = GeneralUtility.CreateGameObject("_____stateMatchingComponent_____", this.transform);
            CreateGameObjectWithScript<InputCategory>("Category: Inputs ____", stateMatchingComponent.transform,ref inputObj, ref inputCategory);
            CreateGameObjectWithScript<InternalEventCategory>("Category: Internal Event ____", stateMatchingComponent.transform, ref internalEventObj, ref internalEventCategory);
            CreateGameObjectWithScript<VariableCategory>("Category: Variable ____", stateMatchingComponent.transform, ref variableObj, ref variableCategory); 
            CreateGameObjectWithScript<DataCategory>("Category: Datas ____", stateMatchingComponent.transform, ref dataObj, ref dataCategory);
            CreateGameObjectWithScript<ActionCategory>("Category: Actions ____", stateMatchingComponent.transform, ref actionObj, ref actionCategory);
            CreateGameObjectWithScript<StateMachineCategory>("Category: StateMachine ____", stateMatchingComponent.transform, ref stateMachineObj,ref stateMachineCategory);
             
            EditorUtility.OpenHierarchy(this.gameObject,true);
            EditorUtility.OpenHierarchy(stateMatchingComponent, true);
        }

        [FoldoutGroup("State Matching Reference"), Button(ButtonSizes.Large), GUIColor(1, 0.4f, 0.4f)]
        void ClearAllStateMatchingComponent()
        {
            
            if(dataCategory?.humanoidInfoDataExtension?.executer != null) dataCategory?.humanoidInfoDataExtension?.executer?.PreDestroy();
            GeneralUtility.RemoveGameObject(stateMatchingComponent);
        }
        
        #endregion
        #region Helper Functions
        public void FindUnityComponent<T>(ref T component) where T : Component
        {
            if (component == null)
            {
                component = GetComponent<T>();
            }
        }

        public void CreateGameObjectWithScript<T>(string objName, Transform parent,ref GameObject objReference,ref T scriptReference) where T: CategoryController
        {
            if (objReference == null) GeneralUtility.CreateGameObjectWithScript<T>(objName, parent, out objReference, out scriptReference, root: this);
            if (scriptReference == null) GeneralUtility.AddStateMatchingComponent<T>(objReference, root: this);
            else scriptReference.Initiate<T>(stateMatchingRoot: this);
            scriptReference.InitiateExtensions();
        }
        #endregion


        public void AnimationEventHaldler(AnimationEvent animationEvent)
        {
            string animationClipName = animationEvent.animatorClipInfo.clip.name;
            int currentEventIndex = animationEvent.intParameter;
            if (internalEventCategory.unityAnimationEventExtension.executer == null) return;
            UnityAnimationEventExtensionExecuter animEventHandler = internalEventCategory.unityAnimationEventExtension.executer;
            UnityAnimationEventGroup getEventGroup = animEventHandler.groupController.GetGroup(animationClipName) as UnityAnimationEventGroup;
            UnityAnimationEventItem getEvent = getEventGroup.items[currentEventIndex] as UnityAnimationEventItem;
            getEvent.InvokeUnityEvent();
        }


    }

}

