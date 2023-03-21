using UnityEngine;

using Sirenix.OdinInspector;
using Sirenix.Serialization;
namespace Nino.NewStateMatching
{
    public class ExecuterInitializerCreater
    {
        [HideInInspector,OdinSerialize] public ExecuterGroup executerGroup;
        [HideInInspector, OdinSerialize] public System.Type executerType;
        public ExecuterInitializerCreater(ExecuterGroup executerGroup,System.Type executerType)
        {
            this.executerGroup = executerGroup;
            this.executerType = executerType;
        }
        [FoldoutGroup("Add New Data Collection"), SerializeField] string collectionName = "New Executer";
        [FoldoutGroup("Add New Data Collection"), Button]
        void CreateNewCollection()
        {
            string newName = GeneralUtility.GetUniqueName(executerGroup.address.GetChildLocalAddresses(), collectionName);
            ExecuterInitializer i = GeneralUtility.AddExecuterInitializer(ref executerGroup.initializers, new ExecuterInitializer(executerGroup, newName, executerType));
            i.Create();
            i.executer.address.localAddress = newName;
            i.executer.address.UpdateGlobalAddressInChild();
        }
        [FoldoutGroup("Add New Data Collection"), Button]
        void RemoveNullInitializer()
        {
            executerGroup.initializers.RemoveAll(x => x.content == null);
            executerGroup.address.UpdateAddressSystem();
        }
    }
}
