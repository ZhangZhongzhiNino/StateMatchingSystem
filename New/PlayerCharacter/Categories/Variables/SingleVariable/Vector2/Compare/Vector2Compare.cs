using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2CompareController : CompareController
    {
        public Vector2CompareController(SMSExecuter executer) : base(executer)
        {
            TF_methods.Add(new TF_Vector2Compare_Equal(executer, "TF Is Equal Input Value"));
            methods.Add(new Vector2Compare_Equal(executer, "Is Equal Input Value"));
        }
    }
    public class TF_Vector2Compare_Equal : TF_CompareMethod
    {
        public TF_Vector2Compare_Equal(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override bool Compare(CompareInput input)
        {
            Vector2Compare_Vector2_Input _input = input as Vector2Compare_Vector2_Input;
            Vector2Value getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as Vector2Value;
            return (getValue.value == _input.inputValue);
        }

        public override CompareInput CreateCompareInput()
        {
            return new Vector2Compare_Vector2_Input(executer);
        }
    }
    public class Vector2Compare_Equal : CompareMethod
    {
        public Vector2Compare_Equal(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override float Compare(CompareInput input)
        {
            Vector2Compare_Vector2Weight_Input _input = input as Vector2Compare_Vector2Weight_Input;
            Vector2Value getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as Vector2Value;
            Vector2 value = getValue.value;
            Vector2 target = _input.inputValue;
            if (value == target) return 0;
            else
            {
                Vector2 difference = target - value;
                return difference.magnitude * _input.weight;
            }
        }

        public override CompareInput CreateCompareInput()
        {
            return new Vector2Compare_Vector2Weight_Input(executer);
        }
    }
    public class Vector2Compare_Vector2_Input : ItemSelectCompareInput
    {
        public Vector2 inputValue;
        public Vector2Compare_Vector2_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
    public class Vector2Compare_Vector2Weight_Input : ItemSelectCompareInput
    {
        public Vector2 inputValue;
        public float weight;
        public Vector2Compare_Vector2Weight_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
}

