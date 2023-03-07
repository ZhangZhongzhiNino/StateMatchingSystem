using Sirenix.OdinInspector;

using System;
using System.Linq;

using UnityEngine;

namespace Nino.NewStateMatching
{
    public class Item
    {
        [TitleGroup("Basic Info",order:-1),LabelWidth(140),PropertyOrder(0),EnableIf("editableInInspector")] public string itemName;
        [TitleGroup("Basic Info"), LabelWidth(125), PropertyOrder(2),ReadOnly] public System.Type valueType;
        [TitleGroup("Basic Info"), LabelWidth(140), PropertyOrder(3), EnableIf("editableInInspector")] public bool actionInput;
        [TitleGroup("Basic Info"), LabelWidth(140), PropertyOrder(4), EnableIf("editableInInspector")] public bool resetWhenEnabled;
        [TitleGroup("Basic Info"), ShowIf("resetWhenEnabled"), LabelWidth(140), PropertyOrder(5), EnableIf("editableInInspector")] public object defaultValue;

        [TitleGroup("Value"), EnableIf("editableInInspector")] public object value;

        public bool editableInInspector;
        
        public Item(System.Type valueType,string itemName)
        {
            this.itemName = itemName;
            this.valueType = valueType;
            value = Activator.CreateInstance(valueType);
            defaultValue = Activator.CreateInstance(valueType);
            resetWhenEnabled = false;
            editableInInspector = false;
        }
        public Item(object value, string itemName)
        {
            this.itemName = itemName;
            this.valueType = value.GetType();
            this.value = value;
            defaultValue = Activator.CreateInstance(valueType);
            editableInInspector = false;
        }
        public bool HaveSameTypeAs(object instance) => valueType.IsAssignableFrom(value.GetType());
        public bool IsType(System.Type type) => type == valueType;
        public T GetValue<T>()
        {
            if (typeof(T) == valueType) return (T)value;
            return default(T);
        }
        public object GetValue(System.Type valueType)
        {
            if (this.valueType == valueType) return value;
            return Activator.CreateInstance(this.valueType);
        }
        public T GetValueCopy<T>()
        {
            T r = default(T);
            if (typeof(T) == valueType)
            {
                r = (T)Activator.CreateInstance(valueType);
                value.GetType().GetProperties().ToList().ForEach(x => x.SetValue(r, x.GetValue(value)));
            }
            return r;
        }
        public object GetValueCopy(System.Type valueType)
        {
            object r = Activator.CreateInstance(this.valueType);
            if (this.valueType == valueType)
            {
                value.GetType().GetProperties().ToList().ForEach(x => x.SetValue(r, x.GetValue(value)));
            }
            return r;
        }
        public T GetDefaultValue<T>() 
        {
            if (typeof(T) == valueType && defaultValue != null) return (T)defaultValue;
            return default(T);
        }
        public object GetDefaultValue(System.Type valueType)
        {
            if (this.valueType == valueType && defaultValue != null) return defaultValue;
            return Activator.CreateInstance(valueType);
        }
        public T GetDefaultValueCopy<T>()
        {
            T r = default(T);
            if (typeof(T) == valueType && defaultValue != null)
            {
                defaultValue.GetType().GetProperties().ToList().ForEach(x => x.SetValue(r, x.GetValue(defaultValue)));
            }
            return r;
        }
        public object GetDefaultValueCopy(System.Type valueType)
        {
            object r = Activator.CreateInstance(valueType);
            if (this.valueType == valueType && defaultValue != null)
            {
                defaultValue.GetType().GetProperties().ToList().ForEach(x => x.SetValue(r, x.GetValue(defaultValue)));
            }
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
                value = GetDefaultValueCopy(valueType);
            }
        }
        [Button,ShowIf("editableInInspector")]
        public void CreateValueIfNull()
        {
            if (value == null) value = Activator.CreateInstance(valueType);
            if (defaultValue == null) value = Activator.CreateInstance(valueType);
            if (value is NeedInitialize _value) _value.Initialize();
            if (defaultValue is NeedInitialize _defaultValue) _defaultValue.Initialize();
        }
    }

}

