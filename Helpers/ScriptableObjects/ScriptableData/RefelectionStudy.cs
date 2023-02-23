using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using System.Reflection;
using System.Reflection.Emit;
class RefelectionStudy : MonoBehaviour
{
    public Vector3 vectorExample;
    [Button]
    static void GetInFunction()
    {
        MethodInfo methodInfo = typeof(RefelectionStudy).GetMethod("MyMethod");
        MethodBody methodBody = methodInfo.GetMethodBody();

        foreach (LocalVariableInfo localVar in methodBody.LocalVariables)
        {
            Debug.Log("Local variable:" + localVar.LocalType + localVar.LocalIndex);
            Debug.Log(localVar);
        }
    }
    [Button]
    void GetInClass()
    {
        Type type = typeof(RefelectionStudy);
        FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            Debug.Log("Field:"+ fieldInfo.FieldType+fieldInfo.Name);
            Debug.Log(fieldInfo.GetValue(this));
        }
    }
    [Button]
    public static void MyMethod(int arg1, string arg2)
    {
        int localInt = 0;
        string localString = "test";
        Debug.Log(arg1 + arg2 + localInt + localString);
    }
}
