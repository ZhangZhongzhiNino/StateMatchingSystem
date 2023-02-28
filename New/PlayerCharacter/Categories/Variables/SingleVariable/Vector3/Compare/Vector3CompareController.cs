using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3CompareController : CompareController
    {
        public Vector3CompareController(SMSExecuter executer) : base(executer)
        {
            TF_methods.Add(new TF_Vector3Compare_Equal(executer, "TF Is Equal Input Value"));
            methods.Add(new Vector3Compare_Equal(executer, "Is Equal Input Value"));
        }
    }
    public class TF_Vector3Compare_Equal : TF_CompareMethod
    {
        public TF_Vector3Compare_Equal(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override bool Compare(CompareInput input)
        {
            Vector3Compare_Vector3_Input _input = input as Vector3Compare_Vector3_Input;
            Vector3Value getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as Vector3Value;
            return (getValue.value == _input.inputValue);
        }

        public override CompareInput CreateCompareInput()
        {
            return new Vector3Compare_Vector3_Input(executer);
        }
    }
    public class Vector3Compare_Equal : CompareMethod
    {
        public Vector3Compare_Equal(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override float Compare(CompareInput input)
        {
            Vector3Compare_Vector3Weight_Input _input = input as Vector3Compare_Vector3Weight_Input;
            Vector3Value getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as Vector3Value;
            Vector3 value = getValue.value;
            Vector3 target = _input.inputValue;
            if (value == target) return 0;
            else
            {
                Vector3 difference = target - value;
                return difference.magnitude * _input.weight;
            }
        }

        public override CompareInput CreateCompareInput()
        {
            return new Vector3Compare_Vector3Weight_Input(executer);
        }
    }
    public class Vector3Compare_Vector3_Input : ItemSelectCompareInput
    {
        public Vector3 inputValue;
        public Vector3Compare_Vector3_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
    public class Vector3Compare_Vector3Weight_Input : ItemSelectCompareInput
    {
        public Vector3 inputValue;
        public float weight;
        public Vector3Compare_Vector3Weight_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
}

