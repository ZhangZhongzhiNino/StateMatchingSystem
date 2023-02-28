using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Nino.NewStateMatching
{
    public class ItemSelectActionInput : ActionInput
    {
        public List<string> items
        {
            get
            {
                return executer.dataController.GetAllItemNames();
            }
        }
        [ValueDropdown("items")] public string selectItem;
        public ItemSelectActionInput(SMSExecuter executer) : base(executer)
        {
        }
    }
    public abstract class ActionInput
    {
        [HideInInspector]public SMSExecuter executer;
        public ActionInput(SMSExecuter executer)
        {
            this.executer = executer;
        }
    }
    [InlineEditor]
    public abstract class ActionMethod
    {
        [ReadOnly] public SMSExecuter executer;
        [ReadOnly] public string actionName;
        public ActionMethod(SMSExecuter script,string actionName)
        {
            this.actionName = actionName;
            this.executer = script;
            demoInput = CreateActionInput();
        }

        
        public abstract void PerformAction(ActionInput input);
        public abstract ActionInput CreateActionInput();


        [SerializeField]ActionInput demoInput;
        [Button(ButtonSizes.Large), GUIColor(1f, 1, 0.4f)]
        void DemoAction()
        {
            PerformAction(demoInput);
        }
    }
    
    public abstract class ActionController
    {
        public List<ActionMethod> actions;

        public ActionController(SMSExecuter executer)
        {
            actions = new List<ActionMethod>();
        }
    }
    public class ActionReference
    {
        public string actionAddress;
        public ActionMethod method;
        [ShowIf("@input!=null")]public ActionInput input;
        [Button(ButtonSizes.Large), GUIColor(1f, 1, 0.4f)]
        public void PerformAction()
        {
            method.PerformAction(input);
        }

        public ActionReference(ActionMethod method)
        {
            this.method = method;
            input = method.CreateActionInput();
            actionAddress = method.executer.address.globalAddress + "/" + method.actionName;

        }
    }

}

