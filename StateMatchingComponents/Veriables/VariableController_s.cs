using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.Variable 
{
    public class VariableController_s : CategoryController, IExtensionController
    {
        public BoolExtension boolValues;
        public IntExtension intValues;
        public FloatExtension floatValues;
        public Vector2Extension vector2Values;
        public Vector3Extension vector3Values;
        public QuaternionExtension quaternionValues;
        public void InitiateExtensions()
        {
            Helpers.SetUpExtensions<BoolExtension>(ref boolValues,"Bool", this.gameObject, root);
            Helpers.SetUpExtensions<IntExtension>(ref intValues, "Int", this.gameObject, root);
            Helpers.SetUpExtensions<FloatExtension>(ref floatValues, "Float", this.gameObject, root);
            Helpers.SetUpExtensions<Vector2Extension>(ref vector2Values, "Vector2", this.gameObject, root);
            Helpers.SetUpExtensions<Vector3Extension>(ref vector3Values, "Vector3", this.gameObject, root);
            Helpers.SetUpExtensions<QuaternionExtension>(ref quaternionValues, "Quaternion", this.gameObject, root);
        }
    }
}



