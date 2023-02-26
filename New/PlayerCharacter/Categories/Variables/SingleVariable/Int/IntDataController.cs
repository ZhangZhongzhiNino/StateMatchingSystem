
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    [System.Serializable]
    public class IntDataController : VariableDataController<IntItem>
    {
        public List<IntItem> intItems = new List<IntItem>();
        [Button("Add Variable", Style = ButtonStyle.Box, ButtonHeight = 40), GUIColor(0.4f, 1, 0.4f)]
        void AddValue(string newItemName, string group, int value)
        {
            AddItem(newItemName);
            IntItem getItem = GetItem(x => x.itemName == newItemName) as IntItem;
            getItem.value = value;
            getItem.group = group;
        }
        protected override string WriteDataType()
        {
            return "Int Variable";
        }

        public override List<Item> GetItemsFromInstance()
        {
            List<Item> r = new List<Item>();
            foreach (IntItem i in intItems) r.Add(i);
            return r;
        }

        public List<string> s= new List<string>();
        public Dictionary<string, IntItem> dic = new Dictionary<string, IntItem>();
    }
}

