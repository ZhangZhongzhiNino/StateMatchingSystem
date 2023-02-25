using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.StateMatching.Helper
{
    public class VeriableGroupPreview<V> : GroupPreview<V>
    {
         
        #region Odin

        #region View Group
        [ShowIfGroup("View Group/List", Condition = "@ _selectGroup != null")]
        [ListDrawerSettings(Expanded = true), ShowInInspector, ReadOnly]
        public override List<string> contains
        {
            get
            {
                List<string> newList = new List<string>();
                newList = groupController.GetItemNameList(selectGroup);
                if (newList == null) return null;
                for (int i = 0; i < newList.Count; i++)
                {
                    newList[i] += ": " + groupController.items[i].value.ToString();
                }
                return newList;
            }
            set { }
        }

        [FoldoutGroup("Items", order: -999), ShowInInspector]
        public override List<string> AllItems
        {
            get
            {
                List<string> newList = new List<string>();
                newList = groupController.GetItemNameList();
                if (newList == null) return null;
                List<string> rList = new List<string>();
                for (int i = 0; i < newList.Count; i++)
                {
                    rList.Add(i.ToString() + ": " + newList[i] + " ---- " + groupController.items[i].value.ToString());
                    
                }
                return rList;
            }
        }
        #endregion


        
        #endregion
    }
}

