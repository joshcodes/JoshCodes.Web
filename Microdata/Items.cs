using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshCodes.Web.Microdata
{
    class Items : IMicrodataNode
    {
        Item [] items;
        int itemsIndex = -1;
        private string name;

        public Items(HtmlAgilityPack.HtmlNode node, string name)
        {
            this.items = GetItems(node).ToArray();
            this.name = name;
        }

        private IEnumerable<Item> GetItems(HtmlAgilityPack.HtmlNode node)
        {
            var itemscopeAttr = node.Attributes.FirstOrDefault((attr) => String.Compare(attr.Name, XmlMicrodataReader.ItemScope, true) == 0);
            if (itemscopeAttr != null)
            {
                yield return new Item(node);
                yield break;
            }
            foreach (var childNode in node.Descendants())
            {
                foreach (var item in GetItems(childNode))
                {
                    yield return item;
                }
            }
        }

        public bool IsEmpty
        {
            get { return items.Length == 0;  }
        }

        public string Name
        {
            get { return this.name; }
        }

        public System.Xml.XmlNodeType NodeType
        {
            get { return System.Xml.XmlNodeType.Element; }
        }

        public bool Read(out IMicrodataNode childItem)
        {
            itemsIndex++;
            if(items.Length <= itemsIndex)
            {
                childItem = null;
                return false;
            }
            childItem = items[itemsIndex];
            return true;
        }

        public string Value
        {
            get { return null; }
        }
    }
}
