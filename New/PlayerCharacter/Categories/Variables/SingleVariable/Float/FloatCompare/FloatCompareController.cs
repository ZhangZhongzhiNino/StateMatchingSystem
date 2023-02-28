using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatCompareController : CompareController
    {
        public FloatCompareController(SMSExecuter executer) : base(executer)
        {
            TF_methods.Add(new TF_FloatCompare_InRange(executer, "TF In Range"));
            TF_methods.Add(new TF_FloatCompare_GreaterThan(executer, "TF Greater Than"));
            TF_methods.Add(new TF_FloatCompare_LessThan(executer, "TF Less Than"));
            TF_methods.Add(new TF_FloatCompare_Equal(executer, "TF Equal"));
            methods.Add(new FloatCompare_InRange(executer, "TF In Range"));
            methods.Add(new FloatCompare_GreaterThan(executer, "TF Greater Than"));
            methods.Add(new FloatCompare_LessThan(executer, "TF Less Than"));
            methods.Add(new FloatCompare_Equal(executer, "Equal"));
        }
    }
    public class FloatCompare_Float_Input : ItemSelectCompareInput
    {
        public float inputValue;
        public FloatCompare_Float_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
    public class FloatCompare_FloatWeight_Input : ItemSelectCompareInput
    {
        public float inputValue;
        public float weight;
        public FloatCompare_FloatWeight_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
    public class FloatCompare_Range_Input : ItemSelectCompareInput
    {
        public Vector2 range;
        public FloatCompare_Range_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
    public class FloatCompare_RangeWeight_Input : ItemSelectCompareInput
    {
        public Vector2 range;
        public float weight;
        public FloatCompare_RangeWeight_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
    public class TF_FloatCompare_InRange : TF_CompareMethod
    {
        public TF_FloatCompare_InRange(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override bool Compare(CompareInput input)
        {
            FloatCompare_Range_Input _input = input as FloatCompare_Range_Input;
            FloatValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as FloatValue;
            float value = getValue.value;
            float x = _input.range.x;
            float y = _input.range.y;
            if(x > y)
            {
                return (value > y && value < x);
            }
            else
            {
                return (value > x && value < y);
            }

        }

        public override CompareInput CreateCompareInput()
        {
            return new FloatCompare_Range_Input(executer);
        }
    }
    public class TF_FloatCompare_GreaterThan : TF_CompareMethod
    {
        public TF_FloatCompare_GreaterThan(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override bool Compare(CompareInput input)
        {
            FloatCompare_Float_Input _input = input as FloatCompare_Float_Input;
            FloatValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as FloatValue;
            return getValue.value > _input.inputValue;
        }

        public override CompareInput CreateCompareInput()
        {
            return new FloatCompare_Float_Input(executer);
        }
    }
    public class TF_FloatCompare_LessThan : TF_CompareMethod
    {
        public TF_FloatCompare_LessThan(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override bool Compare(CompareInput input)
        {
            FloatCompare_Float_Input _input = input as FloatCompare_Float_Input;
            FloatValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as FloatValue;
            return getValue.value < _input.inputValue;
        }

        public override CompareInput CreateCompareInput()
        {
            return new FloatCompare_Float_Input(executer);
        }
    }
    public class TF_FloatCompare_Equal : TF_CompareMethod
    {
        public TF_FloatCompare_Equal(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override bool Compare(CompareInput input)
        {
            FloatCompare_FloatWeight_Input _input = input as FloatCompare_FloatWeight_Input;
            FloatValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as FloatValue;
            return getValue.value == _input.inputValue;
        }

        public override CompareInput CreateCompareInput()
        {
            return new FloatCompare_Float_Input(executer);
        }

    }
    public class FloatCompare_Equal : CompareMethod
    {
        public FloatCompare_Equal(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override float Compare(CompareInput input)
        {
            FloatCompare_FloatWeight_Input _input = input as FloatCompare_FloatWeight_Input;
            FloatValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as FloatValue;
            float difference = Mathf.Abs(_input.inputValue - getValue.value);
            return difference * _input.weight;
        }

        public override CompareInput CreateCompareInput()
        {
            return new FloatCompare_FloatWeight_Input(executer);
        }

    }
    public class FloatCompare_InRange : CompareMethod
    {
        public FloatCompare_InRange(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override float Compare(CompareInput input)
        {
            FloatCompare_RangeWeight_Input _input = input as FloatCompare_RangeWeight_Input;
            FloatValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as FloatValue;
            float value = getValue.value;
            float x = _input.range.x;
            float y = _input.range.y;
            if (x > y)
            {
                if(value > x)
                {
                    float difference = value - x;
                    return difference * _input.weight;
                }
                if(value <y)
                {
                    float difference = y - value;
                    return difference * _input.weight;
                }
                return 0;
            }
            else
            {
                if (value > y)
                {
                    float difference = value - y;
                    return difference * _input.weight;
                }
                if (value < x)
                {
                    float difference = x - value;
                    return difference * _input.weight;
                }
                return 0;
            }

        }

        public override CompareInput CreateCompareInput()
        {
            return new FloatCompare_RangeWeight_Input(executer);
        }
    }
    public class FloatCompare_GreaterThan : CompareMethod
    {
        public FloatCompare_GreaterThan(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override float Compare(CompareInput input)
        {
            FloatCompare_FloatWeight_Input _input = input as FloatCompare_FloatWeight_Input;
            FloatValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as FloatValue;
            float value = getValue.value;
            float target = _input.inputValue;
            if (value > target) return 0;
            else return  (target - value ) * _input.weight;
        }

        public override CompareInput CreateCompareInput()
        {
            return new FloatCompare_FloatWeight_Input(executer);
        }
    }
    public class FloatCompare_LessThan : CompareMethod
    {
        public FloatCompare_LessThan(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override float Compare(CompareInput input)
        {
            FloatCompare_FloatWeight_Input _input = input as FloatCompare_FloatWeight_Input;
            FloatValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as FloatValue;
            float value = getValue.value;
            float target = _input.inputValue;
            if (value < target) return 0;
            else return (value - target) * _input.weight;
        }

        public override CompareInput CreateCompareInput()
        {
            return new FloatCompare_FloatWeight_Input(executer);
        }
    }
}
