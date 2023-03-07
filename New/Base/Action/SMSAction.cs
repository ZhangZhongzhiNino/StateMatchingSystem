using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
namespace Nino.NewStateMatching
{
    public abstract class SMSAction 
    {
        public string actionName;
        public SMSExecuter executer;
        [ReadOnly] public bool haveInput;
        [ReadOnly] public bool needItemReference;
        [ReadOnly] public bool continuous;
        [HideInInspector]public object input;
        [ReadOnly] public System.Type inputType;
        public void PerformAction(object input = null)
        {
            this.input = input;
            ActionMethod();
        }
        public abstract void ActionMethod();
    }

    public class StateMachineActionReference
    {
        public SMSAction actionReference;
        public bool itemReferenceInput;
        public bool eventTriggered;
        public bool itemReferenceForDelay;
        public bool continuousAfterStateFinish;
        public SMSUpdater smsUpdater;
        public float executionDelay;
        public float executionCoolDown;
        

        public System.Type inputType;
        public object staticInput;

        public ItemSelector executionDelayItemSelector;
        public ItemSelector triggerItemSelector;
        public ItemSelector inputItemSelector;

        
        
        public StateMachineActionReference(SMSAction actionReference, SMSUpdater smsUpdater)
        {
            
            this.actionReference = actionReference;

            itemReferenceInput = actionReference.needItemReference;
            eventTriggered = false;
            itemReferenceForDelay = false;
            continuousAfterStateFinish = false;
            
            inputType = actionReference.inputType;
            ResetInput();

            this.smsUpdater = smsUpdater;
            executionDelay = 0;
            executionCoolDown = 0;
            

            AddressData rootAddress = actionReference.executer.address.GetRootAddress();
            executionDelayItemSelector = new ItemSelector(rootAddress, typeof(float));
            triggerItemSelector = new ItemSelector(rootAddress, typeof(UnityEvent));
            inputItemSelector = new ItemSelector(rootAddress, inputType);
        }
        public void PerformAction()
        {
            if (!actionReference.haveInput)
                actionReference.PerformAction();
            else if (itemReferenceInput == true)
                actionReference.PerformAction(inputItemSelector.item.GetValue(actionReference.inputType));
            else actionReference.PerformAction(staticInput);
        }
        public void StateStart()
        {
            if (itemReferenceForDelay == false) 
            {
                executionCoolDown = executionDelay;
            }
            else
            {
                executionCoolDown = executionDelayItemSelector.item.GetValueCopy<float>();
            }
            smsUpdater.timerUpdate.AddListener(CoolDown);
        }
        public void CoolDown()
        {
            executionCoolDown -= 0.2f;
            if(executionCoolDown <0)
            {
                smsUpdater.timerUpdate.RemoveListener(CoolDown);
                CoolDownFinished();
            }
        }
        public void CoolDownFinished()
        {
            if(eventTriggered == false)
            {
                triggerItemSelector.item.GetValue<UnityEvent>().AddListener(EventTriggered);
            }
            else
            {
                if(actionReference.continuous == false) PerformAction();
                else smsUpdater.SMSUpdate.AddListener(PerformAction);
            }
        }
        public void EventTriggered()
        {
            triggerItemSelector.item.GetValue<UnityEvent>().RemoveListener(EventTriggered);
            PerformAction();
        }
        public void StateFinished()
        {
            if (continuousAfterStateFinish) return;
            else
            {
                smsUpdater.timerUpdate.RemoveListener(CoolDown);
                smsUpdater.SMSUpdate.RemoveListener(PerformAction);
            }
        }
        
        public void ResetInput()
        {
            staticInput = Activator.CreateInstance(inputType);
            if(staticInput is ActionInputNeedInitialize instance)
            {
                instance.Initialize();
            }
        }
    }
    public class ItemSelector
    {
        public AddressData rootAddress;
        public AddressData currentAddress;
        public System.Type itemType;
        public string address;
        public Item item;

        public ItemSelector(AddressData rootAddress, System.Type itemType)
        {
            this.rootAddress = rootAddress;
            this.itemType = itemType;
            this.currentAddress = rootAddress;
            address = "";
        }
        public void GoBackToRootAddress()
        {
            currentAddress = rootAddress;
            address = "";
        }
        public void NavigateToChildAddress([ValueDropdown("@currentAddress.GetAllChildLocalAddress()")] string selectChild)
        {
            inputItemNameList = new List<string>();
            if (string.IsNullOrWhiteSpace(selectChild)) return;
            AddressData nextAddress = currentAddress.childs.FirstOrDefault(x => x.localAddress == selectChild);
            if (nextAddress != default(AddressData)) currentAddress = nextAddress;
            if (currentAddress.script is SMSExecuter exe)
            {
                dataController = exe.dataController;
                inputItemNameList = dataController.GetAllItemNamesOfType(itemType);
            }
            address = currentAddress.globalAddress;
        }
        [HideInInspector, SerializeField] DataController dataController;
        [HideInInspector, SerializeField] List<string> inputItemNameList;
        public void SelectItem([ValueDropdown("inputItemNameList")] string selectItem)
        {
            if (string.IsNullOrWhiteSpace(selectItem)) return;
            Item findItem = dataController.GetItem(selectItem);
            if (findItem != null) item = findItem;
        }
    }
    public class SMSUpdater : StateMatchingMonoBehaviour
    {
        public UnityEvent SMSUpdate;
        public float updateInterval;
        public bool updateOn;
        public UnityEvent timerUpdate;
        public float timerInterval;
        public bool timerOn;
        private void Start()
        {
            
        }
        private void OnEnable()
        {
            StartCoroutine(SMSTimerInvoke());
        }
        private void OnDisable()
        {
            StopAllCoroutines();
        }
        public IEnumerator SMSTimerInvoke()
        {
            while (true)
            {
                if(timerOn) timerUpdate?.Invoke();
                yield return new WaitForSeconds(timerInterval);
            }
        }
        public IEnumerator SMSUpdateInvoke()
        {
            while (true)
            {
                if (updateOn) SMSUpdate?.Invoke();
                yield return new WaitForSeconds(updateInterval);
            }
        }
        public override void Initialize()
        {
            timerInterval = 0.2f;
            timerOn = true;
            updateInterval = 0.2f;
            updateOn = true;
        }

        public override void Remove()
        {
            
        }
    }

    public abstract class ActionInputNeedInitialize
    {
        public abstract void Initialize();
    }
}

