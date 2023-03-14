using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Nino.NewStateMatching
{
    public class ExecuterInitializer : SMSMonoBehaviourInitializer
    {
        public ExecuterInitializer(StateMatchingMonoBehaviour creater, string name, Type contentType) : base(creater, name, contentType)
        {
        }

        public SMSExecuter executer { get => content as SMSExecuter; }
        public ExecuterGroup executerGroup { get => creater as ExecuterGroup; }        
        protected override void AssignContentParent() => executer.executerGroup = executerGroup;
        protected override void RemoveNullInCreaterAddress() => executerGroup.address.RemoveNullChildInChild();
        protected override void ResetHierarchy() => executerGroup.ResetHierarchy();
        protected override void UpdateCreaterAddress() => executerGroup.address.AddChild(executer.address);
        protected override string WriteAfterName() => ">";
        protected override string WriteBeforeName() => "<";
        protected override StateMatchingMonoBehaviour TryFindContent()
        {
            AddressData getAddress = executerGroup.address.childs.FirstOrDefault(x => x.localAddress == pureName);
            if (getAddress == default(AddressData)) return null;
            else return getAddress.script;
        }
        protected override void assignAddress()
        {
            AddressData contentAddress = executer.address;
            AddressData createrAddress = executerGroup.address;
            contentAddress.localAddress = pureName;
            createrAddress.AddChild(contentAddress);
            createrAddress.UpdateGlobalAddressInChild();
            executer.InitializeAfterCreateAddress();
        }
    }
    
}

