using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching
{
    public class CompareController
    {
        public List<TF_CompareMethod> TFmethods;
        public List<CompareMethod> methods;
        public CompareController()
        {
            TFmethods = new List<TF_CompareMethod>();
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
        }
        public abstract bool Compare(CompareInput input);
        public abstract CompareInput CreateCompareInput();
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
        }
        public abstract float Compare(CompareInput input);
        public abstract CompareInput CreateCompareInput();
    }
    public abstract class CompareInput
    {
        public string methodAddress;
        public CompareInput(string methodAddress)
        {
            this.methodAddress = methodAddress;
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

