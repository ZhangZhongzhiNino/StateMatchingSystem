using Sirenix.OdinInspector;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using StateMatching.Data;
using StateMatching.Action;
using StateMatching.Helper;
using StateMatching.Input;
using StateMatching.State;
using StateMatching.Variable;
using StateMatching.InternalEvent;
namespace StateMatching
{
    public class StateMatchingRoot : MonoBehaviour
    {
        public StateMatchingGlobalReference stateMatchingGlobalReference;
        public string StateMatchingName;
        
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
        [TitleGroup("State Matching Reference/Script Reference")] public InputController inputController;
        [TitleGroup("State Matching Reference/Script Reference")] public InternalEventController internalEventController;
        [TitleGroup("State Matching Reference/Script Reference")] public VariableController_s variableController;
        [TitleGroup("State Matching Reference/Script Reference")] public DataController dataController;
        [TitleGroup("State Matching Reference/Script Reference")] public ActionController actionController;
        [TitleGroup("State Matching Reference/Script Reference")] public StateMachineController stateMachineController;
        [FoldoutGroup("State Matching Reference"),Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)]
        void SetUpStateMatchingComponent()
        {
            stateMatchingComponent = Helpers.CreateGameObject("_____stateMatchingComponent_____", this.transform);
            CreateGameObjectWithScript<InputController>("Category: Inputs ____", stateMatchingComponent.transform,out inputObj, out inputController);
            CreateGameObjectWithScript<InternalEventController>("Category: Internal Event ____", stateMatchingComponent.transform, out internalEventObj, out internalEventController);
            CreateGameObjectWithScript<VariableController_s>("Category: Variable ____", stateMatchingComponent.transform, out variableObj, out variableController); 
            CreateGameObjectWithScript<DataController>("Category: Datas ____", stateMatchingComponent.transform, out dataObj, out dataController);
            CreateGameObjectWithScript<ActionController>("Category: Actions ____", stateMatchingComponent.transform, out actionObj, out actionController);
            CreateGameObjectWithScript<StateMachineController>("Category: StateMachine ____", stateMatchingComponent.transform,out stateMachineObj,out stateMachineController);
            inputController.InitiateExtensions();
            internalEventController.InitiateExtensions();
            dataController.InitiateExtensions();
            actionController.InitiateExtensions();
            variableController.InitiateExtensions();
            stateMachineController.InitiateExtensions();
            
        }

        [FoldoutGroup("State Matching Reference"), Button(ButtonSizes.Large), GUIColor(1, 0.4f, 0.4f)]
        void ClearAllStateMatchingComponent()
        {
            dataController?.HumanoidInfoDatas?.extension?.PreDestroy();
            Helpers.RemoveGameObject(stateMatchingComponent);
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

        public void CreateGameObjectWithScript<T>(string objName, Transform parent,out GameObject objReference,out T scriptReference) where T: MonoBehaviour,IStateMatchingComponent    
        {
            Helpers.CreateGameObjectWithScript<T>(objName, parent, out objReference, out scriptReference, root: this);
        }
        #endregion


        public void AnimationEventHaldler(AnimationEvent animationEvent)
        {
            string animationClipName = animationEvent.animatorClipInfo.clip.name;
            int currentEventIndex = animationEvent.intParameter;
            Debug.Log($"Animation clip name: {animationClipName}");
            Debug.Log($"Current triggered AnimationEvent index: {currentEventIndex}");
        }
    }

}

