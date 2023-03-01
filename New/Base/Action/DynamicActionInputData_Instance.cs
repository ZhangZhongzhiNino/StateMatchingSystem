using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nino.NewStateMatching
{
    public class DynamicInputData_Vector3 : DynamicActionInputData
    {
        public DynamicInputData_Vector3(string dataName) : base(dataName) { }
        public Vector3 _value;
        public override object value { get => _value; set => _value = (Vector3)value; }
        public override Type GetValueType() => typeof(Vector3);
    }
    public class DynamicInputData_Vector2 : DynamicActionInputData
    {
        public DynamicInputData_Vector2(string dataName) : base(dataName) { }
        public Vector2 _value;
        public override object value { get => _value; set => _value = (Vector2) value; }
        public override Type GetValueType() => typeof(Vector2);
    }
    public class DynamicInputData_Float : DynamicActionInputData
    {
        public DynamicInputData_Float(string dataName) : base(dataName) { }
        public float _value;
        public override object value { get => _value; set => _value = (float) value; }
        public override Type GetValueType() => typeof(float);
    }
    public class DynamicInputData_Int : DynamicActionInputData
    {
        public DynamicInputData_Int(string dataName) : base(dataName) { }
        public int _value;
        public override object value { get => _value; set => _value = (int) value; }
        public override Type GetValueType() => typeof(int);
    }
    public class DynamicInputData_Bool : DynamicActionInputData
    {
        public DynamicInputData_Bool(string dataName) : base(dataName) { }
        public bool _value;
        public override object value { get => _value; set => _value = (bool)value; }
        public override Type GetValueType() => typeof(bool);
    }
    public class DynamicInputData_String : DynamicActionInputData
    {
        public DynamicInputData_String(string dataName) : base(dataName) { }
        public string _value;
        public override object value { get => _value; set => _value = (string)value; }
        public override Type GetValueType() => typeof(string);
    }
}
