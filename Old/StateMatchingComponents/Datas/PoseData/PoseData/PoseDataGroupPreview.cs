using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
using System.Linq;
namespace Nino.StateMatching.Data
{
    public class PoseDataGroupPreview : GroupPreview<PoseDataItem>
    {
        /*public override List<string> AllItems 
        { 
            get 
            {
                List<PoseDataItem> itemList = new List<PoseDataItem>();
                itemList = groupController.items.Cast<PoseDataItem>().ToList();
                if (itemList == null) return null;
                List<string> rList = new List<string>();
                for (int i = 0; i < itemList.Count; i++)
                {
                    rList.Add(i.ToString() + ": " + itemList[i].odinListName);
                }
                return rList;
            } 
        }*/
    }
}

