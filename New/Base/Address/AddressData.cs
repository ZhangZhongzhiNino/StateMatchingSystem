using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
namespace Nino.NewStateMatching
{
    public class AddressData:StateMatchingScriptableObject
    {
        [HideInInspector]public StateMatchingMonoBehaviour script;
        [OnValueChanged("UpdateGlobalAddressInChild"),TitleGroup("Address")]public string localAddress;
        [ReadOnly, TitleGroup("Address")] public string globalAddress;
        [TitleGroup("Pointer")]public AddressData parent;
        [TitleGroup("Pointer"),InlineEditor, ListDrawerSettings(HideAddButton = false,HideRemoveButton = true,DraggableItems = false,ListElementLabelName = "localAddress")]
        [PropertySpace(spaceBefore:0,spaceAfter:10)]
        public List<AddressData> childs;

        public List<string> GetAllChildLocalAddress()
        {
            List<string> r = new List<string>();
            childs.ForEach(x => r.Add(x.localAddress));
            return r;
        }
        protected override void Initialize()
        {
            localAddress = "";
            globalAddress = "";
            parent = null;
            childs = new List<AddressData>();
        }
        public void AddChild(AddressData child)
        {
            if (!childs.Contains(child))
            {
                childs.Add(child);
                child.parent = this;
            }
            UpdateGlobalAddressInChild();
        }
        public void UpdateGlobalAddressOfSystem()
        {
            if (parent != null) parent.UpdateGlobalAddressOfSystem();
            else UpdateGlobalAddressInChild();
        }
        public void UpdateGlobalAddressInChild()
        {
            if (parent == null) globalAddress = localAddress;
            else globalAddress = parent.globalAddress + "/" + localAddress;
            foreach(AddressData child in childs)
            {
                child.UpdateGlobalAddressInChild();
            }
        }
        public void RemoveNullChildsInSystem()
        {
            if (parent != null) parent.RemoveNullChildsInSystem();
            else RemoveNullChildInChild();
        }
        public void RemoveNullChildInChild()
        {
            childs.RemoveAll(x => x.script == null);
            foreach(AddressData child in childs)
            {
                child.RemoveNullChildInChild();
            }
        }
        [TitleGroup("Address"),Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)]
        public void UpdateAddressSystem()
        {
            RemoveNullChildsInSystem();
            UpdateGlobalAddressOfSystem();
        }
        protected override void RunOnEveryEnable()
        {
            
        }
        public List<string> GetChildLocalAddresses()
        {
            List<string> r = new List<string>();
            childs.ForEach(x => r.Add(x.localAddress));
            return r;
        }
        public AddressData GetRootAddress()
        {
            while (parent != null) return parent.GetRootAddress();
            return this;
        }
    }
}

