using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;
namespace Nino.NewStateMatching
{
    public delegate void ActionMethod(object input = null);
    public class SMSAction 
    {
        public string actionName;
        [ReadOnly] public bool haveInput;
        [ReadOnly] public bool needItemReference;
        [ReadOnly] public bool continuous;
        [ReadOnly] public System.Type inputType;
        public ActionMethod PerformAction;
        public SMSAction(string actionName, Action<object> action, System.Type inputType=null, bool haveInput=false, bool needItemReference=false, bool continuous =false)
        {
            this.actionName = actionName;
            this.haveInput = haveInput;
            this.needItemReference = needItemReference;
            this.continuous = continuous;
            this.inputType = inputType;
            this.PerformAction = new ActionMethod(action);
        }
        public SMSAction()
        {

        }
    }
    [LabelWidth(200)]
    public class ActionReference
    {
        [ReadOnly]public SMSAction actionReference;
        [FoldoutGroup("Input")] public bool itemReferenceInput;
        [FoldoutGroup("Input"), ShowIf("@!itemReferenceInput")] public object staticInput;
        [FoldoutGroup("Input"), ShowIf("itemReferenceInput")] public ItemSelector inputItemSelector;
        
        [FoldoutGroup("Delay")] public bool itemReferenceForDelay;
        [FoldoutGroup("Delay"), ShowIf("@!itemReferenceForDelay")] public float executionDelay;
        [FoldoutGroup("Delay"), ShowIf("itemReferenceForDelay")] public ItemSelector executionDelayItemSelector;

        [FoldoutGroup("Other")] public bool eventTriggered;
        [FoldoutGroup("Other"), ShowIf("eventTriggered")] public ItemSelector triggerItemSelector;
        [FoldoutGroup("Other")] public bool continuousAfterStateFinish;

        [HideInInspector]public SMSupdater smsUpdater;
        
        [HideInInspector]public float executionCoolDown;
        

        [HideInInspector] public System.Type inputType;
        

        
        
        

        public ActionReference()
        {

        }
        
        public ActionReference(SMSAction actionReference, SMSupdater smsUpdater,AddressData rootAddress)
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
            

            executionDelayItemSelector = new ItemSelector(rootAddress, typeof(float));
            triggerItemSelector = new ItemSelector(rootAddress, typeof(UnityEvent),group:"Trigger",groupedItem: true);
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
            if(eventTriggered)
            {
                UnityEvent getevent = triggerItemSelector.item.value as UnityEvent;
                getevent.AddListener(EventTriggered);
            }
            else
            {
                if(actionReference.continuous == false) PerformAction();
                else smsUpdater.actionUpdate.AddListener(PerformAction);
            }
        }
        public void EventTriggered()
        {
            UnityEvent getevent = triggerItemSelector.item.value as UnityEvent;
            getevent.RemoveListener(EventTriggered);
            PerformAction();
        }
        public void StateExit()
        {
            if (continuousAfterStateFinish) return;
            else
            {
                smsUpdater.timerUpdate.RemoveListener(CoolDown);
                smsUpdater.actionUpdate.RemoveListener(PerformAction);
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
    public interface NeedInitialize
    {
        public void Initialize();
    }
    public delegate float CompairFucntion(object input, object target);
    public class CompairMethod
    {
        public string compairName;
        public System.Type inputType;
        public System.Type targetType;
        public CompairFucntion Compair;
        public CompairMethod() { }
        public CompairMethod(string compairName,CompairFucntion method, System.Type inputType, System.Type targetType)
        {
            this.compairName = compairName;
            this.inputType = inputType;
            this.targetType = targetType;
            Compair += method;
        }

    }
    public delegate bool TFCompairFucntion(object input, object target);
    public class TFCompairMethod
    {
        public string compairName;
        public System.Type inputType;
        public System.Type targetType;
        public TFCompairFucntion Compair;
        public TFCompairMethod() { }
        public TFCompairMethod(string compairName, TFCompairFucntion method,System.Type inputType, System.Type targetType)
        {
            this.compairName = compairName;
            this.inputType = inputType;
            this.targetType = targetType;
            Compair += method;
        }
    }
    [LabelWidth(200)]
    public class CompairReference
    {
        [ReadOnly] public string compairName;
        [HideInInspector] public CompairMethod compairMethod;
        public float weight;
        [HideInInspector] public System.Type inputType;
        [HideInInspector] public System.Type targetType;
        [FoldoutGroup("Target")] public bool itemReferenceTarget;
        [FoldoutGroup("Target"), ShowIf("@!itemReferenceTarget")] public object target;
        [FoldoutGroup("Target"), ShowIf("itemReferenceTarget")] public ItemSelector targetSelector;
        [FoldoutGroup("Input")] public ItemSelector inputSelector;
        

        public CompairReference(CompairMethod compairMethod,AddressData rootAddress)
        {
            this.compairName = compairMethod.compairName;
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
    [LabelWidth(200)]
    public class TFCompairReference
    {
        [ReadOnly] public string compairName;
        [HideInInspector]public TFCompairMethod compairMethod;
        [HideInInspector] public System.Type inputType;
        [HideInInspector] public System.Type targetType;
        [FoldoutGroup("Target")] public bool itemReferenceTarget;
        [FoldoutGroup("Target"), ShowIf("@!itemReferenceTarget")] public object target;
        [FoldoutGroup("Target"), ShowIf("itemReferenceTarget")] public ItemSelector targetSelector;
        [FoldoutGroup("Input")] public ItemSelector inputSelector;
        

        public TFCompairReference(TFCompairMethod compairMethod, AddressData rootAddress)
        {
            this.compairName = compairMethod.compairName;
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
}

