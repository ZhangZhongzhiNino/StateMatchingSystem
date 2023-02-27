using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public abstract class VariableActionItem<Item, DataController, VariableExecuter> : ActionItemBase<VariableExecuter>
        where VariableExecuter: VariableExecuter<Item, DataController>
        where Item : NewStateMatching.Item, new()
        where DataController : DataController<Item>
    {
        public List<string> itemNameList
        {
            get
            {
                if (script == null || script.dataController == null || script.dataController.items == null) return null;
                return script.dataController.GetAllItemNames();
            }
        }
        [ValueDropdown("itemNameList")] public string selectItem;

        protected VariableActionItem(VariableExecuter script) : base(script)
        {
        }
    }

    public class FloatActionContainer : ActionContainer<FloatExecuter>
    {
        public FloatActionContainer(FloatExecuter script) : base(script)
        {
            actions.Add(new FloatAction_Equal(script));
            actions.Add(new FloatAction_PlusEqual(script));
        }
    }
    public class FloatAction_Equal : VariableActionItem<FloatItem, FloatDataController, FloatExecuter>
    {
        public float inputValue;

        public FloatAction_Equal(FloatExecuter script) : base(script)
        {
            actionName = "Assign New Value";
        }

        public override void AssignVariable(ActionItemBase<FloatExecuter> instance)
        {
            FloatAction_Equal _instance = instance as FloatAction_Equal;
            selectItem = _instance.selectItem;
            inputValue = _instance.inputValue;
        }

        public override void PerformAction()
        {
            FloatItem f = script.dataController.items.Find(x => x.itemName == selectItem);
            f.value = inputValue;
        }
    }
    public class FloatAction_PlusEqual : VariableActionItem<FloatItem, FloatDataController, FloatExecuter>
    {
        public float inputValue;

        public FloatAction_PlusEqual(FloatExecuter script) : base(script)
        {
            actionName = "Value += input Value";
        }

        public override void AssignVariable(ActionItemBase<FloatExecuter> instance)
        {
            FloatAction_Equal _instance = instance as FloatAction_Equal;
            selectItem = _instance.selectItem;
            inputValue = _instance.inputValue;
        }

        public override void PerformAction()
        {
            FloatItem f = script.dataController.items.Find(x => x.itemName == selectItem);
            f.value += inputValue;
        }
    }
    public class BoolActionContainer : ActionContainer<BoolExecuter>
    {
        public BoolActionContainer(BoolExecuter script) : base(script)
        {
            actions.Add(new BoolAction_Equal(script));
            actions.Add(new BoolAction_Not(script));
        }
    }
    public class BoolAction_Equal : VariableActionItem<BoolItem, BoolDataController, BoolExecuter>
    {
        public bool inputValue;

        public BoolAction_Equal(BoolExecuter script) : base(script)
        {
            actionName = "Assign New Value";
        }

        public override void AssignVariable(ActionItemBase<BoolExecuter> instance)
        {
            BoolAction_Equal _instance = instance as BoolAction_Equal;
            selectItem = _instance.selectItem;
            inputValue = _instance.inputValue;
        }

        public override void PerformAction()
        {
            BoolItem f = script.dataController.items.Find(x => x.itemName == selectItem);
            f.value = inputValue;
        }
    }
    public class BoolAction_Not : VariableActionItem<BoolItem, BoolDataController, BoolExecuter>
    {
        public BoolAction_Not(BoolExecuter script) : base(script)
        {
            actionName = "Value = !Value";
        }

        public override void AssignVariable(ActionItemBase<BoolExecuter> instance)
        {
            BoolAction_Equal _instance = instance as BoolAction_Equal;
            selectItem = _instance.selectItem;
        }

        public override void PerformAction()
        {
            BoolItem f = script.dataController.items.Find(x => x.itemName == selectItem);
            f.value = !f.value;
        }
    }
    public class StringActionContainer : ActionContainer<StringExecuter>
    {
        public StringActionContainer(StringExecuter script) : base(script)
        {
            actions.Add(new StringAction_Equal(script));
        }
    }
    public class StringAction_Equal : VariableActionItem<StringItem, StringDataController, StringExecuter>
    {
        public string inputValue;

        public StringAction_Equal(StringExecuter script) : base(script)
        {
            actionName = "Assign New Value";
        }

        public override void AssignVariable(ActionItemBase<StringExecuter> instance)
        {
            StringAction_Equal _instance = instance as StringAction_Equal;
            selectItem = _instance.selectItem;
            inputValue = _instance.inputValue;
        }

        public override void PerformAction()
        {
            StringItem f = script.dataController.items.Find(x => x.itemName == selectItem);
            f.value = inputValue;
        }
    }
    public class Vector2ActionContainer : ActionContainer<Vector2Executer>
    {
        public Vector2ActionContainer(Vector2Executer script) : base(script)
        {
            actions.Add(new Vector2Action_Equal(script));
        }
    }
    public class Vector2Action_Equal : VariableActionItem<Vector2Item, Vector2DataController, Vector2Executer>
    {
        public Vector2 inputValue;

        public Vector2Action_Equal(Vector2Executer script) : base(script)
        {
            actionName = "Assign New Value";
        }

        public override void AssignVariable(ActionItemBase<Vector2Executer> instance)
        {
            Vector2Action_Equal _instance = instance as Vector2Action_Equal;
            selectItem = _instance.selectItem;
            inputValue = _instance.inputValue;
        }

        public override void PerformAction()
        {
            Vector2Item f = script.dataController.items.Find(x => x.itemName == selectItem);
            f.value = inputValue;
        }
    }

    public class Vector3ActionContainer : ActionContainer<Vector3Executer>
    {
        public Vector3ActionContainer(Vector3Executer script) : base(script)
        {
            actions.Add(new Vector3Action_Equal(script));
        }
    }
    public class Vector3Action_Equal : VariableActionItem<Vector3Item, Vector3DataController, Vector3Executer>
    {
        public Vector2 inputValue;

        public Vector3Action_Equal(Vector3Executer script) : base(script)
        {
            actionName = "Assign New Value";
        }

        public override void AssignVariable(ActionItemBase<Vector3Executer> instance)
        {
            Vector3Action_Equal _instance = instance as Vector3Action_Equal;
            selectItem = _instance.selectItem;
            inputValue = _instance.inputValue;
        }

        public override void PerformAction()
        {
            Vector3Item f = script.dataController.items.Find(x => x.itemName == selectItem);
            f.value = inputValue;
        }
    }
}

