using Sirenix.OdinInspector;
using System.Collections.Generic;


namespace Nino.NewStateMatching
{
    public static class OdinUtility
    {
        public static IEnumerable<ValueDropdownItem<string>> ValueDropDownListSelector(List<string> list, List<string> selectList)
        {
            if (list == null) return new List<ValueDropdownItem<string>>();
            var items = new List<ValueDropdownItem<string>>();
            for (int i = 0; i < list.Count; i++)
            {
                if (selectList.Contains(list[i])) continue;
                items.Add(new ValueDropdownItem<string>(list[i], list[i]));
            }
            return items;
        }
    }

}
