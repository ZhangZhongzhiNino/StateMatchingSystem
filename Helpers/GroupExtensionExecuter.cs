using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace StateMatching.Helper
{
    public abstract class GroupExtensionExecuter<T,V> : ExtensionExecuter where T : MonoBehaviour, IGroupItem<T, V> where V : class
    {
        [PropertyOrder(-9999999),FoldoutGroup("Reference")] public GroupController<T, V> groupController;
        [FoldoutGroup("Reference")] public GroupPreview<T, V> groupPreview;
        string typeName;
        public override void Initialize<_T>(_T instance = null, StateMatchingRoot stateMatchingRoot = null)
        {
            base.Initialize<_T>(instance, stateMatchingRoot);
            typeName = typeof(V).Name;
            CreateGroupComponent();
            groupPreview = this.gameObject.AddComponent(GetGroupPreviewType()) as GroupPreview<T, V>;
            groupPreview.Initiate(groupController);
        }
        void CreateGroupComponent()
        {
            GameObject groups = Helpers.CreateGameObject(typeName + " Group Controller", this.transform);
            GameObject items = Helpers.CreateGameObject(typeName + " Items", groups.transform);
            groupController = groups.AddComponent(GetGroupControllerType()) as GroupController<T, V>;
            groupController.Initialize(items);

        }
        public abstract System.Type GetGroupControllerType();
        public abstract System.Type GetGroupPreviewType();
    }

}
