using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class SingleVariableExecuterGroup : ExecuterGroup
    {
        protected override void AddExecuterInitializers()
        {
            
        }

        protected override string WriteLocalAddress()
        {
            return "Single Variable";
        }
        [FoldoutGroup("Add New Data Collection"),SerializeField] string collectionName = "New Collection";
        [FoldoutGroup("Add New Data Collection"), Button]
        void CreateNewCollection()
        {
            string newName = GeneralUtility.GetUniqueName(address.GetChildLocalAddresses(), collectionName);
            ExecuterInitializer i = GeneralUtility.AddExecuterInitializer(ref initializers, new SingleVariableExecuterInitializer(this, newName));
            i.Create();
            i.executer.address.localAddress = newName;
            i.executer.address.UpdateGlobalAddressInChild();
        }
        [FoldoutGroup("Add New Data Collection"), Button]
        void RemoveNullInitializer() => initializers.RemoveAll(x => x.content == null);
    }
}
