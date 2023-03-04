using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Nino.NewStateMatching
{
    public class LabledItem : Item
    {
        [TitleGroup("Basic Info"), LabelWidth(140), PropertyOrder(1)] public string group;
        [TitleGroup("tags")] public List<string> tags;
        public LabledItem(Type valueType, string itemName) : base(valueType, itemName)
        {
            group = "";
            tags = new List<string>();
        }
        public LabledItem(object value, string itemName) : base(value, itemName)
        {
            group = "";
            tags = new List<string>();
        }
        public LabledItem(Type valueType, string itemName,string group) : base(valueType: valueType, itemName)
        {
            this.group = group;
            tags = new List<string>();
        }
        public LabledItem(object value, string itemName,string group) : base(value, itemName)
        {
            this.group = group;
            tags = new List<string>();
        }

        public bool AddTag(string tag)
        {
            if (tags.Contains(tag)) return false;
            tags.Add(tag);
            return true;
        }
        public int AddTags(string[] _tags)
        {
            int i = 0;
            foreach (string s in _tags) if (AddTag(s)) i++;
            return i;
        }
        public int AddTags(List<string> _tags) => AddTags(_tags.ToArray());
        public bool RemoveTag(string tag)
        {
            if (!tags.Contains(tag)) return false;
            tags.Remove(tag);
            return true;
        }
        public int RemoveTags(string[] _tags)
        {
            int i = 0;
            foreach (string s in _tags) if (RemoveTag(s)) i++;
            return i;
        }
        public int RemoveTags(List<string> _tags) => RemoveTags(_tags.ToArray());
        public bool InGroup() => !string.IsNullOrWhiteSpace(group) && !string.IsNullOrEmpty(group);
        public bool InGroup(string group) => this.group == group;
        public bool HaveTag()
        {
            return tags != null && tags.Count != 0;
        }
        public bool HaveTag(string tag) => tags.Contains(tag);
        public bool HaveTags(List<string> _tags)
        {
            foreach (string tag in _tags) if (!tags.Contains(tag)) return false;
            return true;
        }
        public bool HaveTags(string[] _tags) => HaveTags(_tags.ToList());
        public List<string> GetSameTags(List<string> _tags)
        {
            List<string> r = new List<string>();
            foreach (string t in _tags)
            {
                if (tags.Contains(t)) r.Add(t);
            }
            return new List<string>(r);
        }
        public List<string> GetSameTags(string[] _tags) => GetSameTags(_tags.ToList());
        public List<string> GetSameTags(LabledItem _item) => GetSameTags(_item.tags);
        public List<string> HaveMoreTagsThan(List<string> _tags)
        {
            List<string> r = new List<string>();
            foreach (string s in tags)
            {
                if (!_tags.Contains(s)) r.Add(s);
            }
            return new List<string>(r);
        }
        public List<string> HaveMoreTagsThan(string[] _tags) => HaveMoreTagsThan(_tags.ToList());
        public List<string> HaveMoreTagsThan(LabledItem _item) => HaveMoreTagsThan(_item.tags);
        public List<string> DontHaveTagsCompairTo(List<string> _tags)
        {
            List<string> r = new List<string>();
            foreach (string s in _tags)
            {
                if (!tags.Contains(s)) r.Add(s);
            }
            return new List<string>(r);
        }
        public List<string> DontHaveTagsCompairTo(string[] _tags) => DontHaveTagsCompairTo(_tags.ToList());
        public List<string> DontHaveTagsCompairTo(LabledItem _item) => DontHaveTagsCompairTo(_item.tags);
        public static List<string> GetSameTags(LabledItem item1, LabledItem item2) => GetSameTags(new List<LabledItem> { item1, item2 });
        public static List<string> GetSameTags(LabledItem[] items) => GetSameTags(items.ToList());
        public static List<string> GetSameTags(List<LabledItem> items)
        {
            List<string> r = new List<string>();
            Dictionary<string, int> tagCount = new Dictionary<string, int>();
            foreach (LabledItem item in items)
            {
                foreach (string s in item.tags)
                {
                    if (tagCount.ContainsKey(s)) tagCount[s]++;
                    else tagCount[s] = 1;
                }
            }
            foreach (KeyValuePair<string, int> pair in tagCount)
            {
                if (pair.Value == items.Count) r.Add(pair.Key);
            }
            return new List<string>(r);
        }
        public static bool HaveSameTags(List<LabledItem> items)
        {
            List<LabledItem> compairItem = new List<LabledItem>(items);
            int count = compairItem[0].tags.Count;
            List<string> tags0 = compairItem[0].tags;
            for (int i = 0; i < compairItem.Count; i++)
            {
                if (count != compairItem[i].tags.Count) return false;
                if (i != 0 && !compairItem[i].tags.OrderBy(x => x).SequenceEqual(tags0.OrderBy(x => x))) return false;
            }
            return true;
        }
        public static bool HaveSameTags(LabledItem[] items) => HaveSameTags(items.ToList());
        public static bool HaveSameTags(LabledItem A, LabledItem B) => HaveSameTags(new List<LabledItem> { A, B });

    }

}

