using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;

using System; 
using System.Linq;

using UnityEngine;

using UnityEngine.Events;
using Sirenix.Utilities.Editor;

namespace Nino.NewStateMatching
{ 
    public class Item
    {
        [TitleGroup("Basic Info",order:-1),LabelWidth(140), EnableIf("editableInInspector")] public string itemName;
        [TitleGroup("Basic Info"), LabelWidth(125), ReadOnly,OdinSerialize] public System.Type valueType; 
        [TitleGroup("Basic Info"), EnableIf("editableInInspector")] public bool resetWhenEnabled;
        [TitleGroup("Basic Info"), ReadOnly] public bool useOdinSerialization;
        [TitleGroup("Basic Info")] public bool editableInInspector;

        [TitleGroup("Value"), ShowIf("@!useOdinSerialization") , EnableIf("editableInInspector"), SerializeReference] public object Unity_value;
        [TitleGroup("Value"), ShowIf("@useOdinSerialization"), EnableIf("editableInInspector"), OdinSerialize] public object Odin_Value;
        [TitleGroup("Value"), ShowIf("@resetWhenEnabled && !useOdinSerialization"), EnableIf("editableInInspector"), SerializeReference] public object U_DefaultValue;
        [TitleGroup("Value"), ShowIf("@resetWhenEnabled && useOdinSerialization"), EnableIf("editableInInspector"), OdinSerialize] public object Odin_DefaultValue;
        public object defaultValue
        {
            get
            {
                if (useOdinSerialization) return Odin_DefaultValue;
                else return U_DefaultValue;
            }
            set
            {
                if (useOdinSerialization) Odin_DefaultValue = value;
                else U_DefaultValue = value;
            }
        } 
        public object value
        {
            get
            {
                if (useOdinSerialization) return Odin_Value;
                else return Unity_value;
            }
            set
            {
                if (useOdinSerialization) Odin_Value = value;
                else Unity_value = value;
            }
        }

        

        public Item(System.Type valueType, string itemName, bool useOdinSerialization = true)
        {
            this.useOdinSerialization = useOdinSerialization;
            this.itemName = itemName;
            this.valueType = valueType;
            value = Activator.CreateInstance(valueType);
            defaultValue = Activator.CreateInstance(valueType);
            resetWhenEnabled = false;
            editableInInspector = false;
        }
        public Item(object value, string itemName, bool useOdinSerialization = true)
        {
            this.useOdinSerialization = useOdinSerialization;
            this.itemName = itemName;
            this.valueType = value.GetType();
            this.value = value;
            defaultValue = Activator.CreateInstance(valueType);
            editableInInspector = false;
        }
        public bool HaveSameTypeAs(object instance) => valueType.IsAssignableFrom(value.GetType());
        public bool IsType(System.Type type) => type == valueType;
        /*public T GetValue<T>()
        {
            if (typeof(T) == valueType) return (T)value;
            return default(T);
        }*/
        public object GetValue(System.Type valueType)
        {
            if (this.valueType == valueType) return value;
            return Activator.CreateInstance(valueType);
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
                return (T) GeneralUtility.GetValueClone(defaultValue);
            }
            return r;
        }
        public object GetDefaultValueCopy(System.Type valueType) => GeneralUtility.GetValueClone(defaultValue);
        public bool setValue<T>(T newValue)
        {
            if (!IsType(typeof(T))) return false;
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
                value = GeneralUtility.GetValueClone(defaultValue);
            }
        }
        [Button,ShowIf("editableInInspector")]
        public void CreateValueIfNull()
        {
            if (value == null) value = Activator.CreateInstance(valueType);
            if (defaultValue == null) defaultValue = Activator.CreateInstance(valueType);
            if (value is NeedInitialize _value) _value.Initialize();
            if (defaultValue is NeedInitialize _defaultValue) _defaultValue.Initialize();
        }
         
    }

}




