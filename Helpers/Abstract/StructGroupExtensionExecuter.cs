using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.StateMatching.Helper
{
    public abstract class VariableGroupExtensionExecuter<V> : ExtensionExecuter
    {
        [FoldoutGroup("Reference"),PropertyOrder(-999999)] public GroupController<V> groupController;
        [FoldoutGroup("Reference")] public VeriableGroupPreview<V> groupPreview;
        string typeName;
        public override void Initiate<_T>(_T instance = null, StateMatchingRoot stateMatchingRoot = null) //where _T : MonoBehaviour
        {
            base.Initiate<_T>(instance, stateMatchingRoot);
            typeName = typeof(V).Name;
            CreateGroupComponent();
            groupPreview = this.gameObject.AddComponent(GetGroupPreviewType()) as VeriableGroupPreview<V>;
            groupPreview.Initiate(groupController);
        }

        void CreateGroupComponent()
        {
            GameObject groups = GeneralUtility.CreateGameObject(typeName + " Group Controller", this.transform);
            GameObject items = GeneralUtility.CreateGameObject(typeName + " Items", groups.transform);
            groupController = groups.AddComponent(GetGroupControllerType()) as GroupController<V>;
            groupController.Initiate(items);

        }
        public abstract System.Type GetGroupControllerType();
        public abstract System.Type GetGroupPreviewType();
    }

    
}
