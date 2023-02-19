using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
using Sirenix.OdinInspector;
using System;

namespace Nino.StateMatching.Action
{
    public class CharacterControllerExtensionExecuter : ActionGroupExtensionExecuter<CharacterController>
    {
        

        CharacterController currentController;
        List<CharacterController> controlers;


        public override void Initiate<_T>(_T instance = null, StateMatchingRoot stateMatchingRoot = null)
        {
            base.Initiate(instance, stateMatchingRoot);
            if (controlers == null) controlers = new List<CharacterController>();
        }
        [Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)]
        void CreateNewController()
        {
            string newControllerName = "C: "+controlers.Count.ToString();
            GameObject newControllerObj = GeneralUtility.CreateGameObject(newControllerName, this.transform);
            CharacterController newController = newControllerObj.AddComponent<CharacterController>();
            controlers.Add(newController);
            UpdateGroupItems();
            EditorUtility.OpenHierarchy(this.gameObject, true);
        }
        #region Update Items List
        public bool autoUpdate = true;
        [Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)]
        void UpdateGroupItems()
        {
            groupController.RemoveAllInItems(item => true);
            foreach(CharacterController cc in controlers)
            {
                CharacterControllerItem newItem = new CharacterControllerItem(cc.gameObject.name, cc);
                groupController.AddItem(cc.gameObject.name, newItem);
                cc.gameObject.SetActive(false);
            }
        }
        private void Awake()
        {
            autoUpdate = false;
        }
        #endregion
        [Button] public CharacterController SetActiveController(int i)
        {
            autoUpdate = false;
            foreach (CharacterController cc in controlers) cc.gameObject.SetActive(false);
            if (i < 0 || i > controlers.Count)currentController = null;
            else
            {
                currentController = controlers[i];
                currentController.gameObject.SetActive(true);
            }
            return currentController;
        }

        public override Type GetGroupControllerType()
        {
            return typeof(CharacterControllerGroupController);
        }
        public override Type GetGroupPreviewType()
        {
            return typeof(CharacterControllerGroupPreview);
        }
        public override string GetActionGroupName()
        {
            return "Character Controller";
        }

        public override void InitiateActions()
        {

        }
        public override void EditModeUpdateCalls()
        {
            if (autoUpdate) UpdateGroupItems();
        }
    }

}
