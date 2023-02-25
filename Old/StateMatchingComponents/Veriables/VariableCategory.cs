using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
using Sirenix.OdinInspector;

namespace Nino.StateMatching.Variable 
{
    public class VariableCategory : CategoryController
    {
        [FoldoutGroup("Reference")] public BoolExtension boolExtension;
        [FoldoutGroup("Reference")] public IntExtension intExtension;
        [FoldoutGroup("Reference")] public FloatExtension floatExtension;
        [FoldoutGroup("Reference")] public Vector2Extension vector2Extension;
        [FoldoutGroup("Reference")] public Vector3Extension vector3Extension;
        [FoldoutGroup("Reference")] public QuaternionExtension quaternionExtension;
        [FoldoutGroup("Reference")] public StringExtension stringExtension;

        public override string GetActionTypeName()
        {
            return "Variable";
        }

        public override void InitiateExtensions()
        {
            GeneralUtility.SetUpExtensions<BoolExtension>(ref boolExtension,"Bool", this.gameObject, root);
            GeneralUtility.SetUpExtensions<IntExtension>(ref intExtension, "Int", this.gameObject, root);
            GeneralUtility.SetUpExtensions<FloatExtension>(ref floatExtension, "Float", this.gameObject, root);
            GeneralUtility.SetUpExtensions<Vector2Extension>(ref vector2Extension, "Vector2", this.gameObject, root);
            GeneralUtility.SetUpExtensions<Vector3Extension>(ref vector3Extension, "Vector3", this.gameObject, root);
            GeneralUtility.SetUpExtensions<QuaternionExtension>(ref quaternionExtension, "Quaternion", this.gameObject, root);
            GeneralUtility.SetUpExtensions<StringExtension>(ref stringExtension, "String", this.gameObject, root);
        }
    }
}



