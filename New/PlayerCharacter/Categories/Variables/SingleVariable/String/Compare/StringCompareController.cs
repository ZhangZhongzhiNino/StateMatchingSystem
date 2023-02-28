using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringCompareController : CompareController
    {
        public StringCompareController(SMSExecuter executer) : base(executer)
        {
            TF_methods.Add(new TF_StringCompare_Equal(executer, "TF Is Equal Input Value"));
            methods.Add(new StringCompare_Equal(executer, "Is Equal Input Value"));
        }
    }
    public class TF_StringCompare_Equal : TF_CompareMethod
    {
        public TF_StringCompare_Equal(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override bool Compare(CompareInput input)
        {
            StringCompare_String_Input _input = input as StringCompare_String_Input;
            StringValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as StringValue;
            return (getValue.value == _input.inputValue);
        }

        public override CompareInput CreateCompareInput()
        {
            return new StringCompare_String_Input(executer);
        }
    }
    public class StringCompare_Equal : CompareMethod
    {
        public StringCompare_Equal(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override float Compare(CompareInput input)
        {
            StringCompare_StringWeight_Input _input = input as StringCompare_StringWeight_Input;
            StringValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as StringValue;
            if (getValue.value == _input.inputValue) return 0;
            else return _input.weight;
        }

        public override CompareInput CreateCompareInput()
        {
            return new StringCompare_StringWeight_Input(executer);
        }
    }
    public class StringCompare_String_Input : ItemSelectCompareInput
    {
        public string inputValue;
        public StringCompare_String_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
    public class StringCompare_StringWeight_Input : ItemSelectCompareInput
    {
        public string inputValue;
        public float weight;
        public StringCompare_StringWeight_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
}

