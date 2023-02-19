using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.StateMatching.Helper
{
    public abstract class ExtensionExecuter : MonoBehaviour,IStateMatchingComponent 
    {
        [PropertyOrder(-9999999), FoldoutGroup("Reference")] public StateMatchingRoot root;
        [FoldoutGroup("Reference")] public CategoryController category;
        [FoldoutGroup("Reference")] public ActionGroup actionGroup;
        
        public virtual void Initiate<_T>(_T instance = null, StateMatchingRoot stateMatchingRoot = null) where _T : MonoBehaviour
        {
            root = stateMatchingRoot;
            if (actionGroup == null)
            {
                actionGroup = this.gameObject.AddComponent<ActionGroup>();
                actionGroup.Initiate(GetActionGroupName());
            }
            category = GetCategory();
            category.actionType.groups.Add(actionGroup);
            InitiateActions();
            if (!root.editModeUpdater.Contain((System.Action)EditModeUpdateCalls))
                root.editModeUpdater.call += EditModeUpdateCalls;

        }
        public virtual void PreDestroy()
        {
            root.editModeUpdater.call -= EditModeUpdateCalls;
            GeneralUtility.RemoveGameObject(this.gameObject);
        }
        
        
        [Button(ButtonSizes.Large), GUIColor(0.4f, 1, 1),PropertyOrder(-99999999999)]
        void ResetHierarchy()
        {
            EditorUtility.ResetHierachy(root.gameObject, GetCategory().gameObject);
        }


        public abstract CategoryController GetCategory();
        public abstract string GetActionGroupName();
        public abstract void EditModeUpdateCalls();
        public abstract void InitiateActions();
    }
}

