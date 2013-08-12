using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ME3Inventory
{
    public class Consumable : Item
    {
        public Dictionary<String, int> Counts = new Dictionary<String, int>();
        public int Count;

        private static Regex pattern1 = new Regex("(.*?) ([IV]+) - (\\d+)", RegexOptions.Compiled);
        private static Regex pattern2 = new Regex("(.*?) - (\\d+)", RegexOptions.Compiled);

        public Consumable(XElement node, ItemType type)
            : base(node, type)
        {
            IEnumerable<XElement> spans = node.Elements("span");
            if (spans.Count() > 1)
            {
                Obtained = false;
                foreach (XElement span in spans)
                {
                    Match m = pattern1.Match(span.Value);
                    int count = int.Parse(m.Groups[3].Value);
                    Counts[m.Groups[2].Value] = count;
                    if (count > 0)
                        Obtained = true;
                }
                Name = pattern1.Match(Name).Groups[1].Value;
            }
            else
            {
                Match m = pattern2.Match(Name);
                Name = m.Groups[1].Value;
                Count = int.Parse(m.Groups[2].Value);
            }
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Obtained)
                sb.AppendFormat("==[X]== {0}: {1}", Type, Name);
            else
                sb.AppendFormat("--[ ]-- {0}: {1}", Type, Name);
            sb.AppendLine();
            if (Counts.Count <= 0)
            {
                if (Name.Contains("Capacity"))
                    sb.AppendFormat("  Max: {0}", Count);
                else
                    sb.AppendFormat("  Current: {0}", Count);
            }
            else
            {
                foreach(KeyValuePair<String, int> pair in Counts)
                    sb.AppendFormat("  {0}:{1}", pair.Key, pair.Value);
            }
            sb.AppendLine();
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Consumable))
                return false;

            Consumable c = (Consumable)obj;

            if (this.Counts.Count != c.Counts.Count)
                return false;
            
            if (this.Counts.Count > 0)
            {
                foreach (String key in this.Counts.Keys)
                {
                    if (this.Counts[key] != c.Counts[key])
                        return false;
                }
            }    
            
            return this.Name == c.Name && this.Obtained == c.Obtained && this.Count == c.Count;
        }

        public override string Difference(Item other)
        {
            Consumable c = (Consumable)other;

            if (Counts.Count <= 0)
            {
                if (this.Name.Contains("Capacity"))
                    return String.Format("{0} increased from {1} to {2}", this.Name, c.Count, this.Count);
                else
                    return String.Format("Current {0}s {1}creased from {2} to {3}", this.Name, this.Count > c.Count ? "in" : "de", c.Count, this.Count);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}: ", this.Name);
                
                bool isFirst = true;
                foreach (String key in this.Counts.Keys)
                {
                    if (this.Counts[key] != c.Counts[key])
                    {
                        if(!isFirst)
                            sb.Append(", ");
                        isFirst = false;
                        int diff = this.Counts[key] - c.Counts[key];
                        sb.AppendFormat("{0}: {1}{2}", key, diff > 0 ? "+" : "", diff);
                    }
                }
                return sb.ToString();
            }
        }
    }
}
