using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public abstract class VariableItem<T> : Item
    {
        public T value;
        protected override void AssignItem(Item newItem)
        {
            VariableItem<T> _item = newItem as VariableItem<T>;
            value = _item.value;
        }
    }
    public class IntItem : VariableItem<int>
    {
        protected override void InitializeInstance()
        {
            value = 0;
        }
    }
    public class FloatItem : VariableItem<float>
    {
        protected override void InitializeInstance()
        {
            value = 0;
        }
    }
    public class BoolItem : VariableItem<bool>
    {
        protected override void InitializeInstance()
        {
            value = false;
        }
    }
    public class Vector2Item : VariableItem<Vector2>
    {
        protected override void InitializeInstance()
        {
            value = Vector2.zero;
        }
    }
    public class Vector3Item : VariableItem<Vector3>
    {
        protected override void InitializeInstance()
        {
            value = Vector3.zero;
        }
    }
    public class StringItem : VariableItem<string>
    {
        protected override void InitializeInstance()
        {
            value = "";
        }
    }


    public class VariableCollection<Item> : Collection<Item> where Item : NewStateMatching.Item
    {
        protected override void InitializeInstance()
        {

        }
    }

    public class IntCollection : VariableCollection<IntItem> { }
    public class FloatCollection : VariableCollection<FloatItem> { }
    public class BoolCollection : VariableCollection<BoolItem> { }
    public class Vector2Collection : VariableCollection<Vector2Item> { }
    public class Vector3Collection : VariableCollection<Vector3Item> { }
    public class StringCollection : VariableCollection<StringItem> { }


    public abstract class VariableDataController<Item, Collection> : DataController<Item, Collection>
        where Item : NewStateMatching.Item
        where Collection : NewStateMatching.Collection<Item>
    {
        protected override void InitializeInstance()
        {
            
        }
        protected override string WriteHint()
        {
            return "";
        }
    }
    public class IntDataController : VariableDataController<IntItem, IntCollection>
    {
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, int value)
        {
            collection.AddItem(newItemName);
            IntItem getItem = collection.GetItem(x => x.itemName == newItemName);
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "Int Variable";
        }
    }
    public class FloatDataController : VariableDataController<FloatItem, FloatCollection>
    {
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, float value)
        {
            collection.AddItem(newItemName);
            FloatItem getItem = collection.GetItem(x => x.itemName == newItemName);
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "Float Variable";
        }
    }
    public class BoolDataController : VariableDataController<BoolItem, BoolCollection>
    {
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, bool value)
        {
            collection.AddItem(newItemName);
            BoolItem getItem = collection.GetItem(x => x.itemName == newItemName);
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "Bool Variable";
        }
    }
    public class Vector2DataController : VariableDataController<Vector2Item, Vector2Collection>
    {
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, Vector2 value)
        {
            collection.AddItem(newItemName);
            Vector2Item getItem = collection.GetItem(x => x.itemName == newItemName);
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "Vector2 Variable";
        }
    }
    public class Vector3DataController : VariableDataController<Vector3Item, Vector3Collection>
    {
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, Vector3 value)
        {
            collection.AddItem(newItemName);
            Vector3Item getItem = collection.GetItem(x => x.itemName == newItemName);
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "Vector3 Variable";
        }
    }
    public class StringDataController : VariableDataController<StringItem, StringCollection>
    {
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, string value)
        {
            collection.AddItem(newItemName);
            StringItem getItem = collection.GetItem(x => x.itemName == newItemName);
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "String Variable";
        }
    }


    public class VariableExecuter<Item, Collection, DataController>
        : GroupedDataExecuter, IDataExecuter<Item, Collection, DataController>
        where Item : NewStateMatching.Item
        where Collection : NewStateMatching.Collection<Item>
        where DataController : NewStateMatching.DataController<Item, Collection>
    {
        [ShowInInspector] public DataController dataController { get; set; }
        protected override void InitializeDataController()
        {
            dataController = ScriptableObject.CreateInstance<DataController>();
        }

        protected override void PreRemoveDataControllers()
        {

        }
    }
    public class IntExecuter : VariableExecuter<IntItem, IntCollection, IntDataController> { }
    public class FloatExecuter : VariableExecuter<FloatItem, FloatCollection, FloatDataController> { }
    public class BoolExecuter : VariableExecuter<BoolItem, BoolCollection, BoolDataController> { }
    public class Vector2Executer : VariableExecuter<Vector2Item, Vector2Collection, Vector2DataController> { }
    public class Vector3Executer : VariableExecuter<Vector3Item, Vector3Collection, Vector3DataController> { }
    public class StringExecuter : VariableExecuter<StringItem, StringCollection, StringDataController> { }

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

