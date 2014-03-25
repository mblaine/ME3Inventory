using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ME3Inventory
{
    public class Durable : Item
    {
        public int Level;
        public int MaxLevel;

        private static Regex pattern = new Regex("^(.*) [IVX]+$", RegexOptions.Compiled);

        public Durable(XElement node, ItemType type)
            : base(node, type)
        {
            Match level = Regex.Match(node.Elements("img").Last().Attribute("src").Value, "/icons/multiplayer/levels/([0-9]+)\\-([0-9]+).png");
            MaxLevel = int.Parse(level.Groups[1].Value);
            Level = int.Parse(level.Groups[2].Value);
            Match m = pattern.Match(Name);
            if(m.Success)
                Name = m.Groups[1].Value;

            //duplicate names
            if (type == ItemType.Character && (Name == "Quarian Engineer" || Name == "Quarian Infiltrator") && node.Element("img").Attribute("src").Value.Contains("QuarianMale"))
                Name = Name.Replace(" ", " Male ");

            //name changed?
            if( Name == "Vulnerability")
                Name = "Vulnerability VI";
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            if(Obtained)
                sb.AppendFormat("==[X]== {0}: {1}", Type, Name);
            else
                sb.AppendFormat("--[ ]-- {0}: {1}", Type, Name);
            sb.AppendLine();
            sb.AppendFormat("  {0}; Level {1} of {2}", Rarity, Level, MaxLevel);
            sb.AppendLine();
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Durable))
                return false;
            
            Durable d = (Durable) obj;

            return this.Name == d.Name && this.Obtained == d.Obtained && this.Level == d.Level;
        }

        public override string Difference(Item other)
        {
            Durable d = (Durable)other;
            if (this.Obtained != d.Obtained)
                return "Obtained " + this.Name;
            else
            {
                if (this.Level == this.MaxLevel)
                    return String.Format("{0} increased from level {1} to {2} (MAX)", this.Name, d.Level, this.Level);
                else
                    return String.Format("{0} increased from level {1} to {2}", this.Name, d.Level, this.Level);
            }
        }
    }
}
