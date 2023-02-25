using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
namespace Nino.NewStateMatching
{
    [InlineEditor]
    public abstract class InternalActionSelecter : StateMatchingScriptableObject
    {
        protected List<string> categories;
        [OnValueChanged("_OnSelectCategoryChanged")]
        [ValueDropdown("Categories")]
        [SerializeField]
        protected string selectCategory;

        protected bool showExecuterGroup { get => !string.IsNullOrWhiteSpace(selectCategory); }
        protected List<string> ExecuterGroups;
        [OnValueChanged("_OnSelectExecuterGroupChanged")]
        [ValueDropdown("ExecuterGroups")]
        [SerializeField]
        [ShowIf("showExecuterGroup")]
        protected string selectExecuterGroup;

        protected bool showExecuter { get => string.IsNullOrWhiteSpace(selectExecuterGroup); }
        protected List<string> Executers;
        [ValueDropdown("GroupedExecuters")]
        [OnValueChanged("_OnSelectExecuterChanged")]
        [SerializeField]
        [ShowIf("GroupedExecuters")]
        protected string selectExecuter;

        protected bool showAction { get => !string.IsNullOrWhiteSpace(selectExecuter); }
        protected List<string> Actions;
        [OnValueChanged("OnSelectActionChanged")]
        [OnValueChanged("Actions")]
        [SerializeField]
        [ShowIf("showAction")]
        protected string selectAction;

        public string actionAddress;

        protected override void Initialize()
        {
            categories = new List<string>();
            selectCategory = "";
            ExecuterGroups = new List<string>();
            selectExecuterGroup ="";
            Executers = new List<string>();
            selectExecuter = "";
            Actions = new List<string>();
            selectAction = "";
        }
        public abstract void Update();
        protected void _OnSelectCategoryChanged()
        {
            selectExecuterGroup = null;
            OnSelectCategoryChanged();
        }
        protected abstract void OnSelectCategoryChanged();
        protected void _OnSelectExecuterGroupChanged()
        {
            selectExecuter = null;
            OnSelectExecuterGroupChanged();
        }
        protected abstract void OnSelectExecuterGroupChanged();
        protected void _OnSelectExecuterChanged()
        {
            selectAction = null;
            OnSelectExecuterChanged();
        }
        protected abstract void OnSelectExecuterChanged();
        protected void OnSelectActionChanged()
        {
            string r = selectCategory + "/";
            r += selectExecuterGroup + "/";
            r += selectExecuter + "/";
            r += selectAction;
            actionAddress = r;

        }
        
    }

    [InlineEditor]
    public class AddressData:StateMatchingScriptableObject
    {
        [OnValueChanged("UpdateGlobalAddressInChild")]public string localAddress;
        [ReadOnly] public string globalAddress;
        [ReadOnly] public AddressData parent;
        [ReadOnly] public List<AddressData> childs;

        protected override void Initialize()
        {
            localAddress = "";
            globalAddress = "";
            parent = null;
            childs = new List<AddressData>();
        }
        [Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)]
        public void UpdateGlobalAddressOfSystem()
        {
            if (parent != null) parent.UpdateGlobalAddressOfSystem();
            else UpdateGlobalAddressInChild();
        }
        public void UpdateGlobalAddressInChild()
        {
            if (parent == null) globalAddress = localAddress;
            else globalAddress = parent.globalAddress + "/" + localAddress;
            foreach(AddressData addressData in childs)
            {
                UpdateGlobalAddressInChild();
            }
        }
    }
}
namespace Nino.NewStateMatching.PlayerCharacter
{
    public class PlayerCharacterInternalActionSelecter : InternalActionSelecter
    {
        public SMSPlayerCharacterRoot root;

        public override void Update()
        {
            if (root.inputCategory != null && !categories.Contains("Input")) categories.Add("Input");
            else if (root.inputCategory == null && categories.Contains("Input")) categories.Remove("Input");

            if (root.internalEventCategory != null && !categories.Contains("Internal Event")) categories.Add("Internal Event");
            else if (root.internalEventCategory == null && categories.Contains("Internal Event")) categories.Remove("Internal Event");

            if (root.internalEventCategory != null && !categories.Contains("Internal Event")) categories.Add("Internal Event");
            else if (root.internalEventCategory == null && categories.Contains("Internal Event")) categories.Remove("Internal Event");
        }

        protected override void OnSelectCategoryChanged()
        {
            
        }

        protected override void OnSelectExecuterChanged()
        {
            
        }

        protected override void OnSelectExecuterGroupChanged()
        {
            
        }
    }

    public class ActionUtility
    {
        public static PlayerCharacterInternalActionSelecter CreateInternalActionSelector(SMSPlayerCharacterRoot root)
        {
            PlayerCharacterInternalActionSelecter newSelecter = ScriptableObject.CreateInstance<PlayerCharacterInternalActionSelecter>();
            newSelecter.root = root;
            return newSelecter;
        }
    }
}

