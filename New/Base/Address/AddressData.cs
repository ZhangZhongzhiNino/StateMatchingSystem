using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
namespace Nino.NewStateMatching
{
    [InlineEditor]
    public class AddressData:StateMatchingScriptableObject
    {
        [ReadOnly]public StateMatchingMonoBehaviour script;
        [OnValueChanged("UpdateGlobalAddressInChild")]public string localAddress;
        [ReadOnly] public string globalAddress;
        [HideInInlineEditors] public AddressData parent;
        [ListDrawerSettings(HideAddButton = false,HideRemoveButton = true,DraggableItems = false,ListElementLabelName = "localAddress")] public List<AddressData> childs;

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
            if (parent != null) RemoveNullChildsInSystem();
            else RemoveNullChildInChild();
        }
        public void RemoveNullChildInChild()
        {
            childs.RemoveAll(x => x == null);
            foreach(AddressData child in childs)
            {
                child.RemoveNullChildInChild();
            }
        }
        [Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)]
        public void UpdateAddressSystem()
        {
            RemoveNullChildsInSystem();
            UpdateGlobalAddressOfSystem();
        }
    }
}

