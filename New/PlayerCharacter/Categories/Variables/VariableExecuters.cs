using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntItem : Item
    {
        public int value;
        protected override void AssignItem(Item newItem)
        {
            IntItem _item = newItem as IntItem;
            value = _item.value;
        }
        protected override void InitializeInstance()
        {
            value = 0;
        }
    }
    public class FloatItem : Item
    {
        public float value;
        protected override void AssignItem(Item newItem)
        {
            FloatItem _item = newItem as FloatItem;
            value = _item.value;
        }
        protected override void InitializeInstance()
        {
            value = 0;
        }
    }
    public class BoolItem : Item
    {
        public bool value;
        protected override void AssignItem(Item newItem)
        {
            BoolItem _item = newItem as BoolItem;
            value = _item.value;
        }
        protected override void InitializeInstance()
        {
            value = false;
        }
    }
    public class Vector2Item : Item
    {
        public Vector2 value;
        protected override void AssignItem(Item newItem)
        {
            Vector2Item _item = newItem as Vector2Item;
            value = _item.value;
        }
        protected override void InitializeInstance()
        {
            value = Vector2.zero;
        }
    }
    public class Vector3Item : Item
    {
        public Vector3 value;
        protected override void AssignItem(Item newItem)
        {
            Vector3Item _item = newItem as Vector3Item;
            value = _item.value;
        }
        protected override void InitializeInstance()
        {
            value = Vector3.zero;
        }
    }
    public class StringItem : Item
    {
        public string value;
        protected override void AssignItem(Item newItem)
        {
            StringItem _item = newItem as StringItem;
            value = _item.value;
        }
        protected override void InitializeInstance()
        {
            value = "";
        }
    }



    public class IntCollection : Collection<IntItem>
    {
        protected override void InitializeInstance()
        {
            
        }
    }
    public class FloatCollection : Collection<FloatItem>
    {
        protected override void InitializeInstance()
        {

        }
    }
    public class BoolCollection : Collection<BoolItem>
    {
        protected override void InitializeInstance()
        {

        }
    }
    public class Vector2Collection : Collection<Vector2Item>
    {
        protected override void InitializeInstance()
        {

        }
    }
    public class Vector3Collection : Collection<Vector3Item>
    {
        protected override void InitializeInstance()
        {

        }
    }
    public class StringCollection : Collection<StringItem>
    {
        protected override void InitializeInstance()
        {

        }
    }


    public class IntDataController : DataController<IntItem, IntCollection>
    {
        protected override void InitializeInstance()
        {
        }

        protected override string WriteDataType()
        {
            return "Int Variable";
        }

        protected override string WriteHint()
        {
            return "";
        }
    }
    public class FloatDataController : DataController<FloatItem, FloatCollection>
    {
        protected override void InitializeInstance()
        {
        }

        protected override string WriteDataType()
        {
            return "Float Variable";
        }

        protected override string WriteHint()
        {
            return "";
        }
    }
    public class BoolDataController : DataController<BoolItem, BoolCollection>
    {
        protected override void InitializeInstance()
        {
        }

        protected override string WriteDataType()
        {
            return "Bool Variable";
        }

        protected override string WriteHint()
        {
            return "";
        }
    }
    public class Vector2DataController : DataController<Vector2Item, Vector2Collection>
    {
        protected override void InitializeInstance()
        {
        }

        protected override string WriteDataType()
        {
            return "Vector2 Variable";
        }

        protected override string WriteHint()
        {
            return "";
        }
    }
    public class Vector3DataController : DataController<Vector3Item, Vector3Collection>
    {
        protected override void InitializeInstance()
        {
        }

        protected override string WriteDataType()
        {
            return "Vector3 Variable";
        }

        protected override string WriteHint()
        {
            return "";
        }
    }
    public class StringDataController : DataController<StringItem, StringCollection>
    {
        protected override void InitializeInstance()
        {
        }

        protected override string WriteDataType()
        {
            return "String Variable";
        }

        protected override string WriteHint()
        {
            return "";
        }
    }
    

    public class IntExecuter : GroupedDataExecuter, IDataExecuter<IntItem, IntCollection, IntDataController>
    {
        [ShowInInspector] public IntDataController dataController { get; set; }

        protected override void InitializeDataController()
        {
            dataController = ScriptableObject.CreateInstance<IntDataController>();
        }

        protected override void PreRemoveDataControllers()
        {
            
        }
    }
    public class FloatExecuter : GroupedDataExecuter, IDataExecuter<FloatItem, FloatCollection, FloatDataController>
    {
        [ShowInInspector] public FloatDataController dataController { get; set; }

        protected override void InitializeDataController()
        {
            dataController = ScriptableObject.CreateInstance<FloatDataController>();
        }

        protected override void PreRemoveDataControllers()
        {

        }
    }
    public class BoolExecuter : GroupedDataExecuter, IDataExecuter<BoolItem, BoolCollection, BoolDataController>
    {
        [ShowInInspector] public BoolDataController dataController { get; set; }

        protected override void InitializeDataController()
        {
            dataController = ScriptableObject.CreateInstance<BoolDataController>();
        }

        protected override void PreRemoveDataControllers()
        {

        }
    }
    public class Vector2Executer : GroupedDataExecuter, IDataExecuter<Vector2Item, Vector2Collection, Vector2DataController>
    {
        [ShowInInspector] public Vector2DataController dataController { get; set; }

        protected override void InitializeDataController()
        {
            dataController = ScriptableObject.CreateInstance<Vector2DataController>();
        }

        protected override void PreRemoveDataControllers()
        {

        }
    }
    public class Vector3Executer : GroupedDataExecuter, IDataExecuter<Vector3Item, Vector3Collection, Vector3DataController>
    {
        [ShowInInspector] public Vector3DataController dataController { get; set; }

        protected override void InitializeDataController()
        {
            dataController = ScriptableObject.CreateInstance<Vector3DataController>();
        }

        protected override void PreRemoveDataControllers()
        {

        }
    }
    public class StringExecuter : GroupedDataExecuter, IDataExecuter<StringItem, StringCollection, StringDataController>
    {
        [ShowInInspector] public StringDataController dataController { get; set; }

        protected override void InitializeDataController()
        {
            dataController = ScriptableObject.CreateInstance<StringDataController>();
        }

        protected override void PreRemoveDataControllers()
        {

        }
    }

    public class IntExecuterInitializer : GroupedExecuterInitializer<IntExecuter> { }
    public class FloatExecuterInitializer : GroupedExecuterInitializer<FloatExecuter> { }
    public class BoolExecuterInitializer : GroupedExecuterInitializer<BoolExecuter> { }
    public class Vector2ExecuterInitializer : GroupedExecuterInitializer<Vector2Executer> { }
    public class Vector3ExecuterInitializer : GroupedExecuterInitializer<Vector3Executer> { }
    public class StringExecuterInitializer : GroupedExecuterInitializer<StringExecuter> { }


    public class SingleVariableExecuterGroupInitializer : ExecuterGroupInitializer<SingleVariableExecuterGroup> { }
    public class SingleVariableExecuterGroup : ExecuterGroup
    {
        public IntExecuterInitializer intExecuter;
        public FloatExecuterInitializer floatExecuter;
        public BoolExecuterInitializer boolExecuter;
        public StringExecuterInitializer stringExecuter;
        public Vector2ExecuterInitializer vector2Executer;
        public Vector3ExecuterInitializer vector3Executer;
        protected override void InitializeGroupedExecuterInitializers()
        {
            intExecuter = GeneralUtility.InitializeInitializer<IntExecuterInitializer, IntExecuter>(this);
            floatExecuter = GeneralUtility.InitializeInitializer<FloatExecuterInitializer, FloatExecuter>(this);
            boolExecuter = GeneralUtility.InitializeInitializer<BoolExecuterInitializer, BoolExecuter>(this);
            stringExecuter = GeneralUtility.InitializeInitializer<StringExecuterInitializer, StringExecuter>(this);
            vector2Executer = GeneralUtility.InitializeInitializer<Vector2ExecuterInitializer, Vector2Executer>(this);
            vector3Executer = GeneralUtility.InitializeInitializer<Vector3ExecuterInitializer, Vector3Executer>(this);
        }

        protected override void RemoveExecuters()
        {

        }
    }
}

