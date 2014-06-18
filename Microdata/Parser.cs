using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace JoshCodes.Web.Microdata
{
    public class Parser
    {
        private HtmlAgilityPack.HtmlDocument htmlDoc;

        public Parser(Stream stream)
        {
            htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.Load(stream);
        }

        private IEnumerable<HtmlAgilityPack.HtmlNode> GetItemNodes(HtmlAgilityPack.HtmlNode node)
        {
            var itemscopeAttr = node.GetAttributeItemScope();
            if (itemscopeAttr != null)
            {
                yield return node;
                yield break;
            }
            foreach (var childNode in node.Descendants())
            {
                foreach (var item in GetItemNodes(childNode))
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<XmlReader> XmlReaders
        {
            get
            {
                return GetItemNodes(htmlDoc.DocumentNode).
                    Select((item) => new XmlMicrodataReader(item));
            }
        }
    }
}
