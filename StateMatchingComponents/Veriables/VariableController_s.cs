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
            boolValues = Helpers.InitiateExtension<BoolExtension>("BoolValues", this.gameObject, root);
            intValues = Helpers.InitiateExtension<IntExtension>("IntValues", this.gameObject, root);
            floatValues = Helpers.InitiateExtension<FloatExtension>("FloatValues", this.gameObject, root);
            vector2Values = Helpers.InitiateExtension<Vector2Extension>("Vector2Values", this.gameObject, root);
            vector3Values = Helpers.InitiateExtension<Vector3Extension>("Vector3Values", this.gameObject, root);
            quaternionValues = Helpers.InitiateExtension<QuaternionExtension>("QuaternionValues", this.gameObject, root);
        }
    }
}



