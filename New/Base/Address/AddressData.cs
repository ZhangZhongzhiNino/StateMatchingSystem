using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
namespace Nino.NewStateMatching
{
    
    public class AddressData :StateMatchingMonoBehaviour
    {
        [ReadOnly]public StateMatchingMonoBehaviour script;
        [OnValueChanged("UpdateGlobalAddressInChild")]public string localAddress;
        [ReadOnly] public string globalAddress;
        [HideInInlineEditors] public AddressData parent;
        [InlineEditor,ListDrawerSettings(HideAddButton = false,HideRemoveButton = true,DraggableItems = false,ListElementLabelName = "localAddress")] public List<AddressData> childs;
        public override void Initialize()
        {
            localAddress = "";
            globalAddress = "";
            parent = null;
            childs = new List<AddressData>();
        }

        public override void Remove()
        {
            GeneralUtility.RemoveComponent(this);
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
        [Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)]
        public void UpdateAddressSystem()
        {
            RemoveNullChildsInSystem();
            UpdateGlobalAddressOfSystem();
        }

        
    }
}

