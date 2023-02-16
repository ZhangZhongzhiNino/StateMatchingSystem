using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace StateMatching.Helper
{
    public abstract class StructGroupExtensionExecuter<T,V> : ExtensionExecuter where T : MonoBehaviour, IGroupItem<T, V> where V : struct
    {
        public StructGroupController<T, V> groupController;
        string typeName;
        public override void Initialize<_T>(_T instance = null, StateMatchingRoot stateMatchingRoot = null) //where _T : MonoBehaviour
        {
            base.Initialize<_T>(instance, stateMatchingRoot);
            typeName = typeof(V).Name;
            CreateGroupComponent();
            StructGroupPreview<T,V> preview = this.gameObject.AddComponent(GetGroupPreviewType()) as StructGroupPreview<T,V>;
            preview.Initiate(groupController);
        }

        /*public void PreDestroy()
        {
            Helpers.RemoveGameObject(this.gameObject);
        }*/
        void CreateGroupComponent()
        {
            GameObject groups = Helpers.CreateGameObject(typeName + " Group Controller", this.transform);
            GameObject items = Helpers.CreateGameObject(typeName + " Items", groups.transform);
            groupController = groups.AddComponent(GetGroupControllerType()) as StructGroupController<T,V>;
            groupController.Initialize(items);

        }
        public abstract System.Type GetGroupControllerType();
        public abstract System.Type GetGroupPreviewType();
    }

    
}
