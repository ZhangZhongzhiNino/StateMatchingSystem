using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cinemachine;
namespace Nino.NewStateMatching.Action
{
    public class CinemachineExecuter : SMSExecuter
    {
        public Camera mainCamera;
        public GameObject lockViewPrefab;
        public GameObject freeLookPrefab;
        public GameObject currentVC;
        public ItemSelector defaultVCSelector;
        private void Start()
        {
            DisableAllVC();
            SwitchCamera(defaultVCSelector.item.value as GameObject);
        }
        private void FixedUpdate()
        {
            Vector3 forward = mainCamera.transform.forward;
            forward.y = 0;
            Vector3 right = mainCamera.transform.forward;
            right.y = 0;
            dataController.GetItem("Camera Forward").setValue(forward);
            dataController.GetItem("Camera Right").setValue(right);
        }
        public override void InitializeAfterCreateAddress()
        {
            if (lookInputSelector == null) lookInputSelector = new ItemSelector(address.GetRootAddress(), typeof(Vector2));
            if (defaultVCSelector == null) defaultVCSelector = new ItemSelector(address, typeof(GameObject));
        } 
        protected override void InitializeInstance()
        {
            DataUtility.AddAction(dataController, "Swich Camera",SwichCamera, typeof(SwitchCameraInput));
            dataController.AddLabledItem("Camera Forward", "Data",typeof(Vector3));
            dataController.AddLabledItem("Camera Right", "Data", typeof(Vector3));
        } 

        protected override void PreRemoveInstance()
        {
            
        }
        void DisableAllVC()
        {
            List<Item> getItems = dataController.items.FindAll(x => x.IsType(typeof(GameObject)));
            foreach (Item item in getItems)
            {
                GameObject getObj = item.value as GameObject;
                getObj?.SetActive(false);
            }
        }
        void SwitchCamera(GameObject nextVC)
        {
            if(currentVC != null) currentVC.SetActive(false);
            currentVC = nextVC;
            if (currentVC != null) currentVC.SetActive(true);
        }
        void SwichCamera(object _input)
        {
            SwitchCameraInput input = _input as SwitchCameraInput;
            Item getItem = dataController.GetItem(input.nextVC);
            GameObject getObj = getItem?.value as GameObject;
            if(getObj != null)
            {
                SwitchCamera(getObj);
            }
        }
        [Button] void CreateFreeLookCamera(string newCameraName)
        {
            newCameraName = "Free Look: " + newCameraName;
            List<string> existNames = dataController.GetAllItemNamesOfGroup("Free Look");
            newCameraName = GeneralUtility.GetUniqueName(existNames, newCameraName);
            GameObject newCam = Instantiate(freeLookPrefab);
            newCam.name = "Free Look: "+newCameraName;
            newCam.transform.SetParent(this.transform);
            GeneralUtility.ResetLocalTransform(newCam);
            LabledItem newCameraItem = new LabledItem(newCam, newCameraName, "Free Look");
            GeneralUtility.RemoveGameObject(newCameraItem.defaultValue as GameObject);
            dataController.AddItem(newCameraItem);


        }
        [Button] void CreateLockViewCamera(string newCameraName)
        {
            newCameraName = "Lock View: " + newCameraName;
            List<string> existNames = dataController.GetAllItemNamesOfGroup("Lock View");
            newCameraName = GeneralUtility.GetUniqueName(existNames, newCameraName);
            GameObject newCam = Instantiate(lockViewPrefab);
            newCam.name = "Lock View: " + newCameraName;
            newCam.transform.SetParent(this.transform);
            GeneralUtility.ResetLocalTransform(newCam);
            LabledItem newCameraItem = new LabledItem(newCam, newCameraName, "Lock View");
            GeneralUtility.RemoveGameObject(newCameraItem.defaultValue as GameObject);
            dataController.AddItem(newCameraItem);


        }


        //Look Input Settings
        [FoldoutGroup("Look Input")]public ItemSelector lookInputSelector; 
        private Vector2 lookInput
        {
            get
            {
                return (Vector2)lookInputSelector?.item?.value;
            }
            set
            {
                lookInputSelector.item.value = value;
            }
        }
        string Xname;
        string Yname;
        [FoldoutGroup("Look Input")] public float Xsynsitivity;
        [FoldoutGroup("Look Input")] public float Ysynsitivity;
        [Button(ButtonSizes.Medium)]
        [GUIColor(0.6f, 1f, 0.6f)]
        [FoldoutGroup("Look Input")]
        void RestoreDefaultValue()
        {
            Xsynsitivity = 0.2f;
            Ysynsitivity = 0.2f;
        }
        private void OnEnable()
        {
            Xname = "X";
            Yname = "Y";
            CinemachineCore.GetInputAxis = getInputAxis;
        } 
        private float getInputAxis(string axisName)
        {
            float r;
            if (axisName == Xname)
            {
                r = lookInput.x;
                Vector2 tempV2 = lookInput;
                tempV2.x = 0;
                lookInput = tempV2;
                return r * Xsynsitivity;
            }
            if (axisName == Yname)
            {
                r = lookInput.y;
                Vector2 tempV2 = lookInput;
                tempV2.y = 0;
                lookInput = tempV2;
                return r * Ysynsitivity;
            }
            return Input.GetAxis(axisName);
        }
    }
    public class SwitchCameraInput
    {
        public string nextVC ="";
        public SwitchCameraInput()
        {
            nextVC = "";
        }
    }
}

