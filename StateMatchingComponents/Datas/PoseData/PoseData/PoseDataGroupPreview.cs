using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.Data
{
    public class PoseDataGroupPreview : GroupPreview<PoseData,PoseData>
    {
        public override List<string> AllItems 
        { 
            get 
            {
                List<PoseData> itemList = new List<PoseData>();
                itemList = groupController.items;
                if (itemList == null) return null;
                List<string> rList = new List<string>();
                for (int i = 0; i < itemList.Count; i++)
                {
                    rList.Add(i.ToString() + ": " + itemList[i].odinListName);
                }
                return rList;
            } 
        }
    }
}

