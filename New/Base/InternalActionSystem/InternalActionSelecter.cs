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
}

