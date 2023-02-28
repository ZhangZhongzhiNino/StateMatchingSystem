using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntCompareController : CompareController
    {
        public IntCompareController(SMSExecuter executer) : base(executer)
        {
            TF_methods.Add(new TF_IntCompare_InRange(executer, "TF In Range"));
            TF_methods.Add(new TF_IntCompare_GreaterThan(executer, "TF Greater Than"));
            TF_methods.Add(new TF_IntCompare_LessThan(executer, "TF Less Than"));
            TF_methods.Add(new TF_IntCompare_Equal(executer, "TF Equal"));
            methods.Add(new IntCompare_InRange(executer, "In Range"));
            methods.Add(new IntCompare_GreaterThan(executer, "Greater Than"));
            methods.Add(new IntCompare_LessThan(executer, "Less Than"));
            methods.Add(new IntCompare_Equal(executer, "Equal"));
        }
    }
    public class IntCompare_Int_Input : ItemSelectCompareInput
    {
        public int inputValue;
        public IntCompare_Int_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
    public class IntCompare_IntWeight_Input : ItemSelectCompareInput
    {
        public int inputValue;
        public int weight;
        public IntCompare_IntWeight_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
    public class IntCompare_Range_Input : ItemSelectCompareInput
    {
        public Vector2Int range;
        public IntCompare_Range_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
    public class IntCompare_RangeWeight_Input : ItemSelectCompareInput
    {
        public Vector2Int range;
        public int weight;
        public IntCompare_RangeWeight_Input(SMSExecuter executer) : base(executer)
        {
        }
    }
    public class TF_IntCompare_InRange : TF_CompareMethod
    {
        public TF_IntCompare_InRange(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override bool Compare(CompareInput input)
        {
            IntCompare_Range_Input _input = input as IntCompare_Range_Input;
            IntValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as IntValue;
            int value = getValue.value;
            int x = _input.range.x;
            int y = _input.range.y;
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
            return new IntCompare_Range_Input(executer);
        }
    }
    public class TF_IntCompare_GreaterThan : TF_CompareMethod
    {
        public TF_IntCompare_GreaterThan(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override bool Compare(CompareInput input)
        {
            IntCompare_Int_Input _input = input as IntCompare_Int_Input;
            IntValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as IntValue;
            return getValue.value > _input.inputValue;
        }

        public override CompareInput CreateCompareInput()
        {
            return new IntCompare_Int_Input(executer);
        }
    }
    public class TF_IntCompare_LessThan : TF_CompareMethod
    {
        public TF_IntCompare_LessThan(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override bool Compare(CompareInput input)
        {
            IntCompare_Int_Input _input = input as IntCompare_Int_Input;
            IntValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as IntValue;
            return getValue.value < _input.inputValue;
        }

        public override CompareInput CreateCompareInput()
        {
            return new IntCompare_Int_Input(executer);
        }
    }
    public class TF_IntCompare_Equal : TF_CompareMethod
    {
        public TF_IntCompare_Equal(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override bool Compare(CompareInput input)
        {
            IntCompare_IntWeight_Input _input = input as IntCompare_IntWeight_Input;
            IntValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as IntValue;
            return getValue.value == _input.inputValue;
        }

        public override CompareInput CreateCompareInput()
        {
            return new IntCompare_Int_Input(executer);
        }

    }
    public class IntCompare_Equal : CompareMethod
    {
        public IntCompare_Equal(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override float Compare(CompareInput input)
        {
            IntCompare_IntWeight_Input _input = input as IntCompare_IntWeight_Input;
            IntValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as IntValue;
            int difference = Mathf.Abs(_input.inputValue - getValue.value);
            return difference * _input.weight;
        }

        public override CompareInput CreateCompareInput()
        {
            return new IntCompare_IntWeight_Input(executer);
        }

    }
    public class IntCompare_InRange : CompareMethod
    {
        public IntCompare_InRange(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override float Compare(CompareInput input)
        {
            IntCompare_RangeWeight_Input _input = input as IntCompare_RangeWeight_Input;
            IntValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as IntValue;
            int value = getValue.value;
            int x = _input.range.x;
            int y = _input.range.y;
            if (x > y)
            {
                if(value > x)
                {
                    int difference = value - x;
                    return difference * _input.weight;
                }
                if(value <y)
                {
                    int difference = y - value;
                    return difference * _input.weight;
                }
                return 0;
            }
            else
            {
                if (value > y)
                {
                    int difference = value - y;
                    return difference * _input.weight;
                }
                if (value < x)
                {
                    int difference = x - value;
                    return difference * _input.weight;
                }
                return 0;
            }

        }

        public override CompareInput CreateCompareInput()
        {
            return new IntCompare_RangeWeight_Input(executer);
        }
    }
    public class IntCompare_GreaterThan : CompareMethod
    {
        public IntCompare_GreaterThan(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override float Compare(CompareInput input)
        {
            IntCompare_IntWeight_Input _input = input as IntCompare_IntWeight_Input;
            IntValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as IntValue;
            int value = getValue.value;
            int target = _input.inputValue;
            if (value > target) return 0;
            else return  (target - value ) * _input.weight;
        }

        public override CompareInput CreateCompareInput()
        {
            return new IntCompare_IntWeight_Input(executer);
        }
    }
    public class IntCompare_LessThan : CompareMethod
    {
        public IntCompare_LessThan(SMSExecuter executer, string methodName) : base(executer, methodName)
        {
        }

        public override float Compare(CompareInput input)
        {
            IntCompare_IntWeight_Input _input = input as IntCompare_IntWeight_Input;
            IntValue getValue = executer.dataController.items.Find(x => x.itemName == _input.selectItem).value as IntValue;
            int value = getValue.value;
            int target = _input.inputValue;
            if (value < target) return 0;
            else return (value - target) * _input.weight;
        }

        public override CompareInput CreateCompareInput()
        {
            return new IntCompare_IntWeight_Input(executer);
        }
    }
}
