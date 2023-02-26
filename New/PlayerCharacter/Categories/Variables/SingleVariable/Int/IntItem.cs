using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntItem : Item
    {
        public int value;
        protected override void AssignItem(Item newItem)
        {
            IntItem _item = newItem as IntItem;
            value = _item.value;
        }
        public IntItem() : base()
        {
            
        }
    }
}

