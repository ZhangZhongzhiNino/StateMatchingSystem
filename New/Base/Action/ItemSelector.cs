using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using System.Reflection;
namespace Nino.NewStateMatching
{
    public class ItemSelector: ICloneable
    {
        [HideInInspector,OdinSerialize] public AddressData rootAddress;
        [HideInInspector, OdinSerialize] public AddressData currentAddress;
        [HideInInspector, OdinSerialize] public System.Type itemType;
        [ReadOnly]public string address;
        public Item item 
        {
            get
            {
                if (string.IsNullOrWhiteSpace(selectItem)) return null;
                if (dataController == null) return null;
                return  dataController.GetItem(selectItem);
            }
        }
        public bool groupedItem;
        public bool tagedItem;
        public string group;
        public List<string> tags;

        public ItemSelector(
            AddressData rootAddress, 
            System.Type itemType,
            bool groupedItem = false, 
            bool tagedItem = false,
            string group = null, 
            List<string> tags = null)
        {
            this.rootAddress = rootAddress;
            this.itemType = itemType;
            this.currentAddress = rootAddress;
            address = "";
            this.groupedItem = groupedItem;
            this.tagedItem = tagedItem;
            this.group = group;
            this.tags = tags;
            if (tags == null) tags = new List<string>();
        }
        public ItemSelector GetClone()
        {
            return (ItemSelector) SerializationUtility.CreateCopy(this);
        } 
        
        [Button(ButtonSizes.Large),GUIColor(1,0.4f,0.4f),ShowIf("@rootAddress != currentAddress")]
        public void GoBackToRootAddress()
        {
            currentAddress = rootAddress;
            address = "";
        }
        [Button(Style = ButtonStyle.Box,ButtonHeight = 40), GUIColor(0.4f, 1f, 0.4f), ShowIf("@currentAddress.childs.Count !=0")]
        public void NavigateToChildAddress([ValueDropdown("@currentAddress.GetAllChildLocalAddress()")] string selectChild)
        {
            if (string.IsNullOrWhiteSpace(selectChild)) return;
            AddressData nextAddress = currentAddress.childs.FirstOrDefault(x => x.localAddress == selectChild);
            if (nextAddress != default(AddressData)) currentAddress = nextAddress;
            address = currentAddress.globalAddress; 
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
        [HideInInspector] DataController dataController
        {
            get
            {
                if (currentAddress.script is SMSExecuter exe) return exe.dataController;
                else return null;
            }
        }
        [HideInInspector] List<string> selectItemNameList
        {
            get
            {
                if(currentAddress==null || currentAddress.script == null) return new List<string> ();
                if (currentAddress.script is SMSExecuter exe)
                {
                    List<string> r = dataController.GetAllItemNamesOfType(itemType);
                    if (groupedItem) r = r.Intersect(dataController.GetAllItemNamesOfGroup(group)).ToList();
                    if (tagedItem)
                    {
                        foreach (string tag in tags)
                        {
                            r = r.Intersect(dataController.GetAllItemNamesOfTag(tag)).ToList();
                        }
                    }
                    return r;
                }
                else return new List<string>();
            }
        }
        [ShowIf("@selectItemNameList.Count !=0"),ValueDropdown("selectItemNameList")]
        public string selectItem;
    }

   
}

