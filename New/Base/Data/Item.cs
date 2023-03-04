using Sirenix.OdinInspector;

using System;
using System.Linq;

using UnityEngine;

namespace Nino.NewStateMatching
{
    public class Item
    {
        [TitleGroup("Basic Info",order:-1),LabelWidth(140),PropertyOrder(0)] public string itemName;
        [TitleGroup("Basic Info"), LabelWidth(125), PropertyOrder(2),ReadOnly] public System.Type valueType;
        [TitleGroup("Basic Info"), LabelWidth(140), PropertyOrder(3)] public bool actionInput;
        [TitleGroup("Basic Info"), LabelWidth(140), PropertyOrder(4)] public bool resetWhenEnabled;
        [TitleGroup("Basic Info"), ShowIf("resetWhenEnabled"), LabelWidth(140), PropertyOrder(5)] public object defaultValue;

        [TitleGroup("Value")] public object value;

        
        public Item(System.Type valueType,string itemName)
        {
            this.itemName = itemName;
            this.valueType = valueType;
            value = Activator.CreateInstance(valueType);
            defaultValue = Activator.CreateInstance(valueType);
            resetWhenEnabled = false;
        }
        public Item(object value, string itemName)
        {
            this.itemName = itemName;
            this.valueType = value.GetType();
            this.value = value;
            defaultValue = Activator.CreateInstance(valueType);
        }
        public bool HaveSameTypeAs(object instance) => valueType.IsAssignableFrom(value.GetType());
        public bool IsType(System.Type type) => type == valueType;
        public T getValue<T>()
        {
            T r = default(T);
            if (typeof(T) == valueType) r = (T)value;
            return r;
        }
        public bool setValue<T>(T newValue)
        {
            if (IsType(typeof(T))) return false;
            this.value = newValue;
            return true;
        }
        public bool setValue(object newValue)
        {
            if (!HaveSameTypeAs(newValue)) return false;
            this.value = newValue;
            return true;
        }
        public void TryResetValue()
        {
            if (resetWhenEnabled)
            {
                value = Activator.CreateInstance(valueType);
                defaultValue.GetType().GetProperties().ToList().ForEach(x => x.SetValue(value, x.GetValue(defaultValue)));
            }
        }
        public void CreateValueIfNull()
        {
            if (value == null) value = Activator.CreateInstance(valueType);
            if (defaultValue == null) value = Activator.CreateInstance(valueType);
        }
    }

}

