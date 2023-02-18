using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
using Sirenix.OdinInspector;
using System;

namespace StateMatching.Action
{
    public class CharacterControllerExecuter : ActionGroupExtensionExecuter<CharacterController>
    {
        public override Type GetGroupControllerType()
        {
            return typeof(CharacterControllerGroupController);
        }
        public override Type GetGroupPreviewType()
        {
            return typeof(CharacterControllerGroupPreview);
        }


        CharacterController currentController;
        List<CharacterController> controlers;


        public override void Initialize<_T>(_T instance = null, StateMatchingRoot stateMatchingRoot = null)
        {
            base.Initialize(instance, stateMatchingRoot);
            if (controlers == null) controlers = new List<CharacterController>();
        }
        [Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)]
        void CreateNewController()
        {
            string newControllerName = "C: "+controlers.Count.ToString();
            GameObject newControllerObj = Helpers.CreateGameObject(newControllerName, this.transform);
            CharacterController newController = newControllerObj.AddComponent<CharacterController>();
            controlers.Add(newController);
            UpdateGroupItems();
            Helpers.OpenHierarchy(this.gameObject, true);
        }
        #region Update Items List
        [FoldoutGroup("Update Items List")]
        public bool autoUpdate = true;
        private void OnDrawGizmos()
        {
            if (autoUpdate) UpdateGroupItems();
        }
        [FoldoutGroup("Update Items List")]
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
        [Button]
        public CharacterController SetActiveController(int i)
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

    }

}
