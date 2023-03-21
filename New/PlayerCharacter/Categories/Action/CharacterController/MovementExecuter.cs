using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.Action
{
    public class MovementExecuter : SMSExecuter
    {
        public GameObject rootObj;
        public GameObject ccPrefab;
        public CharacterController currentCC;
        public ItemSelector defaultCCSelector;
        public ItemSelector forwardV3Selector;
        public ItemSelector rightV3Selector;
        public bool moving;
        public float lookForwardSpeed
        {
            get
            {
                return (float)dataController.GetItem("Look Forward Speed").value;
            }
            set
            {
                dataController.GetItem("Look Forward Speed").setValue(value);
            }
        }
        float moveSpeed
        {
            get
            {
                return (float)dataController.GetItem("Move Speed").value;
            }
            set
            {
                dataController.GetItem("Move Speed").setValue(value);
            }
        }
        Vector2 direction
        {
            get
            {
                return(Vector2) dataController.GetItem("Move Direction").value;
            }
            set
            {
                dataController.GetItem("Move Direction").setValue(value);
            }
        }
        Vector3 forwardVectro
        {
            get
            {
                return (Vector3) forwardV3Selector.item.value;
            }
        }
        Vector3 rightVector
        {
            get
            {
                return (Vector3)rightV3Selector.item.value;
            }
        }
        Vector3 moveVector
        {
            get
            {
                return forwardVectro * direction.y + rightVector * direction.x;
            }
        }
        private void Start()
        {
            DisableAllController();
            CharacterController defaultCC = defaultCCSelector.item.value as CharacterController;
            SwitchController(defaultCC);
        }
        private void FixedUpdate()
        {
            if (!moving) return;
            Move();
        }
        public override void InitializeAfterCreateAddress()
        {
            StateMatchingRoot getSMSroot = address.GetRootAddress().script as StateMatchingRoot;
            rootObj = getSMSroot.objRoot;
            defaultCCSelector = new ItemSelector(address, typeof(CharacterController));
            forwardV3Selector = new ItemSelector(address.GetRootAddress(), typeof(Vector3));
            rightV3Selector = new ItemSelector(address.GetRootAddress(), typeof(Vector3));


        }
        protected override void InitializeInstance()
        {
            moving = false;
            dataController.AddItem("Move Speed", typeof(float));
            dataController.AddItem("Move Direction", typeof(Vector2));
            dataController.AddItem("Look Forward Speed", typeof(float));
            DataUtility.AddAction(dataController, "Switch Controller", SwitchController, typeof(SwitchControllerInput));
            DataUtility.AddAction(dataController, "Disable All Controller",(object i)=> DisableAllController());
            DataUtility.AddAction(dataController, "Start Move", (object i) => StartMove());
            DataUtility.AddAction(dataController, "Stop Move", (object i) => StopMove());
            DataUtility.AddAction(dataController, "Move", (object i) => Move(), continuous: true);
            DataUtility.AddAction(dataController, "Set Move Direction", SetMoveDirection, typeof(Vector2));
            DataUtility.AddAction(dataController, "Set Move Speed", SetSpeed, typeof(float));
            DataUtility.AddAction(dataController, "Set Look Speed", SetLookSpeed, typeof(float));
        } 
        protected override void PreRemoveInstance()
        {
            
        }

        [Button]
        void CreateCC(string newControllerName)
        {
            List<string> getExistNames = dataController.GetAllItemNamesOfGroup("Controller");
            newControllerName = GeneralUtility.GetUniqueName(getExistNames, newControllerName);
            GameObject newControllerObj = Instantiate(ccPrefab);
            newControllerObj.name = newControllerName;
            newControllerObj.transform.SetParent(this.transform);
            GeneralUtility.ResetLocalTransform(newControllerObj);
            CharacterController newController = newControllerObj.GetComponentInChildren<CharacterController>();
            LabledItem newControllerItem = new LabledItem(newController, newControllerName, "Controller");
            
            dataController.AddItem(newControllerItem);
        } 
        void SwitchController(CharacterController newController)
        {
            if (currentCC != null) currentCC.gameObject.SetActive(false);
            currentCC = newController;
            if (currentCC != null) currentCC.gameObject.SetActive(true);
        }
        void SwitchController(object _input)
        {
            SwitchControllerInput input = _input as SwitchControllerInput;
            CharacterController getNextCC = dataController.GetItem(input.newControllerName).value as CharacterController;
            SwitchController(getNextCC);
            currentCC.transform.position = Vector3.zero;
        }
        void DisableAllController()
        {
            List<Item> getControlerItems = dataController.items.FindAll(x => x.valueType == typeof(CharacterController));
            getControlerItems.ForEach(x =>
            {
                CharacterController getCC = x.value as CharacterController;
                getCC.gameObject.SetActive(false);
            });
        }
        public void StartMove()
        {
            currentCC.transform.position = Vector3.zero;
            moving = true;
        }
        public void StopMove() => moving = false;
        public void SetRootObjPosition()
        {
            rootObj.transform.position = currentCC.transform.position;
            currentCC.transform.position = Vector3.zero;
        }
        public void Move()
        {
            //Vector3 moveVector = forwardVectro * direction.y + rightVector * direction.x;
            currentCC.Move(moveVector * moveSpeed * Time.fixedDeltaTime);
            SetRootObjPosition();
        }
        public void LookForward()
        {
            Quaternion targetDirection = Quaternion.LookRotation(moveVector);
            float _lookForwardSpeed = 1;
            if (lookForwardSpeed < 0) _lookForwardSpeed = 0;
            else if (lookForwardSpeed > 1) _lookForwardSpeed = 1;
            currentCC.transform.rotation = Quaternion.Lerp(currentCC.transform.rotation, targetDirection, _lookForwardSpeed);
        }
        public void SetMoveDirection(object input)
        {
            Vector3 _input =(Vector2) input;
            direction = _input;
        }
        public void SetSpeed(object input)
        {
            float _input = (float)input;
            moveSpeed = _input;
        }
        public void SetLookSpeed(object input)
        {
            float _input = (float)input;
            lookForwardSpeed = _input;
        }
    }
    public class SwitchControllerInput
    {
        public string newControllerName;
        public SwitchControllerInput()
        {
            newControllerName = "";
        }
    }
}

