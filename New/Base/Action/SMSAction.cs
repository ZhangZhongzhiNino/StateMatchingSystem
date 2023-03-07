using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
namespace Nino.NewStateMatching
{
    public delegate void ActionMethod(object input = null);
    public class SMSAction 
    {
        public string actionName;
        public SMSExecuter executer;
        [ReadOnly] public bool haveInput;
        [ReadOnly] public bool needItemReference;
        [ReadOnly] public bool continuous;
        [ReadOnly] public System.Type inputType;
        public ActionMethod PerformAction;
        public SMSAction(string actionName, Action<object> action)
        {
            this.actionName = actionName;
            this.PerformAction = new ActionMethod(action);
        }
    }
    public class MyClass : MonoBehaviour
    {
        SMSAction newAction;
        public void exeampleFunction(object input)
        {
            Debug.Log(transform.position);
        }
        private void Start()
        {
            newAction = new SMSAction("newName", exeampleFunction);
            newAction.PerformAction?.Invoke();
        }
    }
    public class ActionReference
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
        
        
        
        public ActionReference(SMSAction actionReference, SMSUpdater smsUpdater)
        {
            
            this.actionReference = actionReference;

            itemReferenceInput = actionReference.needItemReference;
            eventTriggered = false;
            itemReferenceForDelay = false;
            continuousAfterStateFinish = false;
            
            inputType = actionReference.inputType;
            InitializeInput();

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
        public void StateEnter()
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
        public void StateExit()
        {
            if (continuousAfterStateFinish) return;
            else
            {
                smsUpdater.timerUpdate.RemoveListener(CoolDown);
                smsUpdater.SMSUpdate.RemoveListener(PerformAction);
            }
        }
        
        public void InitializeInput()
        {
            staticInput = Activator.CreateInstance(inputType);
            if(staticInput is NeedInitialize staticInputNeedInitialize)
            {
                staticInputNeedInitialize.Initialize();
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
    public interface NeedInitialize
    {
        public void Initialize();
    }
    public abstract class CompairMethod
    {
        public System.Type inputType;
        public System.Type targetType;
        public abstract float Compair(object input, object target);
    }
    public abstract class TFCompairMethod
    {
        public System.Type inputType;
        public System.Type targetType;
        public abstract bool Compair(object input, object target);
    }
    public class CompairReference
    {
        public CompairMethod compairMethod;
        public float weight;
        public System.Type inputType;
        public System.Type targetType;
        public bool itemReferenceTarget;
        public object target;
        public ItemSelector inputSelector;
        public ItemSelector targetSelector;

        public CompairReference(CompairMethod compairMethod,AddressData rootAddress)
        {
            this.compairMethod = compairMethod;
            weight = 1;
            inputType = compairMethod.inputType;
            targetType = compairMethod.targetType;
            itemReferenceTarget = false;
            InitializeTarget();
            inputSelector = new ItemSelector(rootAddress, inputType);
            targetSelector = new ItemSelector(rootAddress, targetType);
        }
        public float GetDifference()
        {
            if (itemReferenceTarget) return weight * compairMethod.Compair(inputSelector.item.GetValue(inputType), targetSelector.item.GetValue(targetType));
            else return weight * compairMethod.Compair(inputSelector.item.GetValue(inputType), target);
        }
        public void InitializeTarget()
        {
            target = Activator.CreateInstance(targetType);
            if(target is NeedInitialize targetNeedInitialize)
            {
                targetNeedInitialize.Initialize();
            }
        }
        
    }
    public class TFCompairReference
    {
        public TFCompairMethod compairMethod;
        public System.Type inputType;
        public System.Type targetType;
        public bool itemReferenceTarget;
        public object target;
        public ItemSelector inputSelector;
        public ItemSelector targetSelector;

        public TFCompairReference(TFCompairMethod compairMethod, AddressData rootAddress)
        {
            this.compairMethod = compairMethod;
            inputType = compairMethod.inputType;
            targetType = compairMethod.targetType;
            itemReferenceTarget = false;
            InitializeTarget();
            inputSelector = new ItemSelector(rootAddress, inputType);
            targetSelector = new ItemSelector(rootAddress, targetType);
        }
        public bool GetBool()
        {
            if (itemReferenceTarget) return  compairMethod.Compair(inputSelector.item.GetValue(inputType), targetSelector.item.GetValue(targetType));
            else return compairMethod.Compair(inputSelector.item.GetValue(inputType), target);
        }
        public void InitializeTarget()
        {
            target = Activator.CreateInstance(targetType);
            if (target is NeedInitialize targetNeedInitialize)
            {
                targetNeedInitialize.Initialize();
            }
        }
    }
    public class SMSState: NeedInitialize
    {
        public string stateName;
        public AddressData rootAddress;
        public SMSUpdater smsUpdater;
        public List<TFCompairReference> tfCompairs;
        public List<CompairReference> compairs;
        public List<ActionReference> actions;
        public ItemSelector tfCompairSelector;
        public ItemSelector compairSelector;
        public ItemSelector actionSelector;
        public void Initialize()
        {
            if (tfCompairs == null) tfCompairs = new List<TFCompairReference>();
            if (compairs == null) compairs = new List<CompairReference>();
            if (actions == null) actions = new List<ActionReference>();
            if (tfCompairSelector == null) tfCompairSelector = new ItemSelector(rootAddress, typeof(TFCompairMethod));
            if (compairSelector == null) compairSelector = new ItemSelector(rootAddress, typeof(CompairMethod));
            if (actionSelector == null) actionSelector = new ItemSelector(rootAddress, typeof(SMSAction));
        }
        public SMSState(string stateName, SMSUpdater smsUpdater, AddressData rootAddress)
        {
            this.rootAddress = rootAddress;
            this.stateName = stateName;
            this.smsUpdater = smsUpdater;
            Initialize();
        }
        public bool AbleToTransisst()
        {
            foreach(TFCompairReference tfCompair in tfCompairs)
            {
                if (!tfCompair.GetBool()) return false;
            }
            return true;
        }
        public float GetPreference()
        {
            float sumWeight = 0;
            float sumDifference = 0;
            compairs.ForEach(x => {
                sumWeight += x.weight;
                sumDifference += x.GetDifference();
            });
            return sumDifference / sumWeight;
        }

        public void EnterState()
        {
            actions.ForEach(x => x.StateEnter());
        }
        public void ExistState()
        {
            actions.ForEach(x => x.StateExit());
        }

        [Button]
        public void AddTFCompair()
        {
            tfCompairs.Add(
                new TFCompairReference(
                    tfCompairSelector.item.GetValue(typeof(TFCompairMethod)) as TFCompairMethod,
                    rootAddress));
        }
        [Button]
        public void AddCompair()
        {
            compairs.Add(
                new CompairReference(
                    compairSelector.item.GetValue(typeof(CompairMethod)) as CompairMethod,
                    rootAddress));
        }
        [Button]
        public void AddAction()
        {
            actions.Add(
                new ActionReference(
                    actionSelector.item.GetValue(typeof(SMSAction)) as SMSAction,
                    smsUpdater));
        }
    }
}

