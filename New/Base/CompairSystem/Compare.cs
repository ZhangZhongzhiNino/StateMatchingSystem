using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching
{
    public class CompareController
    {
        public List<TF_CompareMethod> TF_methods;
        public List<CompareMethod> methods;
        public CompareController(SMSExecuter executer)
        {
            TF_methods = new List<TF_CompareMethod>();
            methods = new List<CompareMethod>();
        }
    }
    public abstract class TF_CompareMethod
    {
        [ReadOnly] public SMSExecuter executer;
        [ReadOnly] public string methodName;
        public TF_CompareMethod(SMSExecuter executer, string methodName)
        {
            this.executer = executer;
            this.methodName = methodName;
            demoInput = CreateCompareInput();
        }
        public abstract bool Compare(CompareInput input);
        public abstract CompareInput CreateCompareInput();

        public CompareInput demoInput;
        [Button(ButtonSizes.Large),GUIColor(1,1,0.4f)]
        bool DemoCompare()
        {
            return Compare(demoInput);
        }
    }
    public abstract class CompareMethod
    {
        [ReadOnly] public SMSExecuter executer;
        [ReadOnly] public string methodName;
        public CompareInput input;
        public CompareMethod(SMSExecuter executer, string methodName)
        {
            this.executer = executer;
            this.methodName = methodName;
            demoInput = CreateCompareInput();
        }
        public abstract float Compare(CompareInput input);
        public abstract CompareInput CreateCompareInput();

        public CompareInput demoInput;
        [Button(ButtonSizes.Large), GUIColor(1, 1, 0.4f)]
        float DemoCompare()
        {
            return Compare(demoInput);
        }
    }
    public abstract class ItemSelectCompareInput : CompareInput
    {
        [HideInInspector] public SMSExecuter executer;
        public List<string> items
        {
            get
            {
                return executer.dataController.GetAllItemNames();
            }
        }
        [ValueDropdown("items")] public string selectItem;
        public ItemSelectCompareInput(SMSExecuter executer): base()
        {
            this.executer = executer;
        }
    }
    public abstract class CompareInput
    {
        public CompareInput()
        {

        }
    }
    public class TF_CompareTarget
    {
        public string compareAddress;
        public TF_CompareMethod method;
        [ShowIf("@input != null")]public CompareInput input;
        [Button(ButtonSizes.Large), GUIColor(1f, 1, 0.4f)]
        public bool Compair()
        {
            return method.Compare(input);
        } 

        public TF_CompareTarget(TF_CompareMethod method)
        {
            this.method = method;
            this.input = method.CreateCompareInput();
            this.compareAddress = method.executer.address.globalAddress + "/" + method.methodName;
        }
    }
    public class CompareTarget
    {
        public string compareAddress;
        public CompareMethod method;
        [ShowIf("@input != null")] public CompareInput input;
        [Button(ButtonSizes.Large), GUIColor(1f, 1, 0.4f)]
        public float Compair()
        {
            return method.Compare(input);
        }

        public CompareTarget(CompareMethod method)
        {
            this.method = method;
            this.input = method.CreateCompareInput();
            this.compareAddress = method.executer.address.globalAddress + "/" + method.methodName;
        }
    }


}

