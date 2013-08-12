using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ME3Inventory
{
    public abstract class Item
    {
        public String Name;
        public ItemType Type;
        public Rarity Rarity;
        public bool Obtained;

        public Item()
        {
        }

        public Item(XElement node, ItemType type)
        {
            Type = type;
            Rarity = (Rarity)Enum.Parse(typeof(Rarity), Regex.Match(node.Attribute("style").Value, "/icons/multiplayer/cards/([a-zA-Z]+)-o[nf]+\\.png").Groups[1].Value, true);
            Name = node.Element("span").Value;
            Obtained = !node.Attribute("class").Value.Contains("off");
        }

        public abstract String Difference(Item other);
    }
}
