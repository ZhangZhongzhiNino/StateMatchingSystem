using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Nino.NewStateMatching
{
    public class ExecuterGroupInitializer : SMSMonoBehaviourInitializer
    {
        public ExecuterGroupInitializer(StateMatchingMonoBehaviour creater, string name, Type contentType) : base(creater, name, contentType)
        {
        }

        public ExecuterGroup executerGroup { get => content as ExecuterGroup; }
        public ExecuterCategory executerCategory { get => creater as ExecuterCategory; }
        
        protected override void AssignContentParent() => executerGroup.executerCategory = executerCategory;
        protected override void RemoveNullInCreaterAddress() => executerCategory.address.RemoveNullChildInChild();
        protected override void ResetHierarchy() => executerCategory.ResetHierarchy();
        protected override void UpdateCreaterAddress() => executerCategory.address.AddChild(executerGroup.address);
        protected override string WriteAfterName() => "";
        protected override string WriteBeforeName() => "____";
        protected override StateMatchingMonoBehaviour TryFindContent()
        {
            AddressData getAddress = executerCategory.address.childs.FirstOrDefault(x => x.localAddress == pureName);
            if (getAddress == default(AddressData)) return null;
            else return getAddress.script;
        }
        protected override void assignAddress()
        {
            AddressData contentAddress = executerGroup.address;
            AddressData createrAddress = executerCategory.address;
            contentAddress.localAddress = pureName;
            createrAddress.AddChild(contentAddress);
            createrAddress.UpdateGlobalAddressInChild();
        }
    }
}

