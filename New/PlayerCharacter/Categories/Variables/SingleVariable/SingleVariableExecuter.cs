using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter
{
    public class SingleVariableExecuter : SMSExecuter
    {
        [HideInInspector]public List<System.Type> variableTypes;
        [FoldoutGroup("Add item"), SerializeField] string itemName;
        [FoldoutGroup("Add item"), ValueDropdown("variableTypes"), SerializeField] System.Type variableType;
        [FoldoutGroup("Add item"), SerializeField] bool labled = false;
        [FoldoutGroup("Add item"), ShowIf("labled"), SerializeField] string groupName = "New Group";
        [FoldoutGroup("Add item"), SerializeField] bool resetWhenEnabled = false;
        [FoldoutGroup("Add item"), SerializeField] bool actionInput = false;
        [FoldoutGroup("Add item"), Button, SerializeField]
        void AddNewItem()
        {
            Item newItem = dataController.AddItem(itemName, variableType, labled, groupName);
            newItem.resetWhenEnabled = resetWhenEnabled;
        }

        [Button]
        protected override void InitializeInstance()
        {
            variableTypes = new List<System.Type>();
            variableTypes.Add(typeof(bool));
            variableTypes.Add(typeof(int));
            variableTypes.Add(typeof(float));
            variableTypes.Add(typeof(Vector2));
            variableTypes.Add(typeof(Vector3));
            variableTypes.Add(typeof(string));
            dataController.editableInInspector = true;

            DataUtility.AddAction(dataController, "SetFloat",  SetFloat, typeof(SetFloatInput));
            DataUtility.AddAction(dataController, "SetInt",  SetInt, typeof(SetIntInput));
            DataUtility.AddAction(dataController, "SetBool",  SetBool, typeof(SetBoolInput));
            DataUtility.AddAction(dataController, "SetVector2",  SetVector2, typeof(SetVector2Input));
            DataUtility.AddAction(dataController, "SetVector3",  SetVector3, typeof(SetVector3Input));
            DataUtility.AddAction(dataController, "SetString",  SetString, typeof(SetStringInput));

            DataUtility.AddTFCompair(dataController, "TFC_FloatEqual", (a, b) => TFC_FloatEqual(a, b), typeof(float), typeof(float));
            DataUtility.AddCompair(dataController, "C_FloatEqual", (a, b) => C_FloatEqual(a, b), typeof(float), typeof(float));
        }

        protected override void PreRemoveInstance()
        {

        }
        public override void InitializeAfterCreateAddress()
        {

        }
        public void SetFloat(object input)
        {
            SetFloatInput _input =(SetFloatInput) input;
            dataController.GetItem(_input.itemName).setValue(_input.newValue);
        }
        public void SetInt(object input)
        {
            SetIntInput _input = (SetIntInput)input;
            dataController.GetItem(_input.itemName).setValue(_input.newValue);
        }
        public void SetBool(object input)
        {
            SetBoolInput _input = (SetBoolInput)input;
            dataController.GetItem(_input.itemName).setValue(_input.newValue);
        }
        public void SetVector2(object input)
        {
            SetVector2Input _input = (SetVector2Input)input;
            dataController.GetItem(_input.itemName).setValue(_input.newValue);
        }
        public void SetVector3(object input)
        {
            SetVector3Input _input = (SetVector3Input)input;
            dataController.GetItem(_input.itemName).setValue(_input.newValue);
        }
        public void SetString(object input)
        {
            SetStringInput _input = (SetStringInput)input;
            dataController.GetItem(_input.itemName).setValue(_input.newValue);
        }

        public bool TFC_FloatEqual(object input, object target)
        {
            float _input = (float) input;
            float _target = (float) target;
            return _input == _target;
        }
        public float C_FloatEqual(object input, object target)
        {
            float _input = (float)input;
            float _target = (float)target;
            return Mathf.Abs(_input - _target);
        }

    }

    public struct SetFloatInput
    {
        public string itemName;
        public float newValue;
    }
    public struct SetIntInput
    {
        public string itemName;
        public int newValue;
    }
    public struct SetBoolInput
    {
        public string itemName;
        public bool newValue;
    }
    public struct SetVector2Input
    {
        public string itemName;
        public Vector2 newValue;
    }
    public struct SetVector3Input
    {
        public string itemName;
        public Vector3 newValue;
    }
    public struct SetStringInput
    {
        public string itemName;
        public string newValue;
    }
     
}

