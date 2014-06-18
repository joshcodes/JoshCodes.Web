using System;
using System.Linq;

namespace JoshCodes.Web.Microdata
{
    static class NodeExtensions
    {
        private const string ItemScope = "itemscope";
        private const string ItemType = "itemtype";
        private const string ItemProp = "itemprop";

        public static HtmlAgilityPack.HtmlAttribute GetAttribute(this HtmlAgilityPack.HtmlNode node, string name)
        {
            var attribute = node.Attributes.FirstOrDefault((attr) => String.Compare(attr.Name, name, true) == 0);
            return attribute;
        }

        public static HtmlAgilityPack.HtmlAttribute GetAttributeItemProperty(this HtmlAgilityPack.HtmlNode node)
        {
            var attr = node.GetAttribute(ItemProp);
            return attr;
        }

        public static HtmlAgilityPack.HtmlAttribute GetAttributeItemScope(this HtmlAgilityPack.HtmlNode node)
        {
            var attr = node.GetAttribute(ItemScope);
            return attr;
        }

        public static HtmlAgilityPack.HtmlAttribute GetAttributeItemType(this HtmlAgilityPack.HtmlNode node)
        {
            var attr = node.GetAttribute(ItemType);
            return attr;
        }

        

    }
}
