using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolCompareController : CompareController
    {
        public BoolCompareController(SMSExecuter executer) : base(executer)
        {
            TF_methods.Add(new TF_BoolCompare_Equal(executer, "TF Is Equal Input Value"));
            methods.Add(new BoolCompare_Equal(executer, "Is Equal Input Value"));
        }
    }
    public class TF_BoolCompare_Equal : TF_CompareMethod
    {
        public TF_BoolCompare_Equal(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override bool Compare(CompareInput input)
        {
            BoolCompare_Bool_Input _input = input as BoolCompare_Bool_Input;
            BoolValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as BoolValue;
            return (getValue.value == _input.inputValue);
        }

        public override CompareInput CreateCompareInput()
        {
            return new BoolCompare_Bool_Input(executer);
        }
    }
    public class BoolCompare_Equal : CompareMethod
    {
        public BoolCompare_Equal(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override float Compare(CompareInput input)
        {
            BoolCompare_BoolWeight_Input _input = input as BoolCompare_BoolWeight_Input;
            BoolValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as BoolValue;
            if (getValue.value == _input.inputValue) return 0;
            else return _input.weight;
        }

        public override CompareInput CreateCompareInput()
        {
            return new BoolCompare_BoolWeight_Input(executer);
        }
    }
    public class BoolCompare_Bool_Input : ItemSelectCompareInput
    {
        public bool inputValue;
        public BoolCompare_Bool_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
    public class BoolCompare_BoolWeight_Input : ItemSelectCompareInput
    {
        public bool inputValue;
        public float weight;
        public BoolCompare_BoolWeight_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
}

