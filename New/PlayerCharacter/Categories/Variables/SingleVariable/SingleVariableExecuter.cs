using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching.PlayerCharacter
{
    public class SingleVariableExecuter : SMSExecuter
    {
        [HideInInspector] List<System.Type> variableTypes;
        [FoldoutGroup("Add item"),SerializeField]string itemName;
        [FoldoutGroup("Add item"),ValueDropdown("variableTypes"), SerializeField] System.Type variableType;
        [FoldoutGroup("Add item"), SerializeField] bool labled = false;
        [FoldoutGroup("Add item"),ShowIf("labled"), SerializeField] string groupName = "New Group";
        [FoldoutGroup("Add item"), SerializeField] bool resetWhenEnabled = false;
        [FoldoutGroup("Add item"), SerializeField] bool actionInput = false;
        [FoldoutGroup("Add item"),Button, SerializeField]
        void AddNewItem()
        {
            Item newItem = dataController.AddItem(itemName, variableType, labled, groupName);
            newItem.resetWhenEnabled = resetWhenEnabled;
            newItem.actionInput = actionInput;
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
        }

        protected override void PreRemoveInstance()
        {
            
        }
    }
}

