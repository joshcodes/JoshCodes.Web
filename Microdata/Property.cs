using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JoshCodes.Web.Microdata
{
    class Property : IMicrodataNode
    {
        /*
         * This class has to replicate some behavior that does not map to XML well.
         * In reading the equivolent XML document a text property will result in three reads:
         * 
         * Example: <name>Bill Miller</name>
         * Read 1 -> NodeType = Element, Value = "", LocalName = "name"
         * Read 2 -> NodeType = Text, Value = "Bill Miller", LocalName = ""
         * Read 3 -> NodeType = EndElement, Value = "", LocalName = "name"
         * 
         * Therefore nodeType keeps the state and the responses are modified accordingly.
         */

        private HtmlAgilityPack.HtmlNode node;
        private HtmlAgilityPack.HtmlAttribute itemscopeAttr;

        public Property(HtmlAgilityPack.HtmlNode node, HtmlAgilityPack.HtmlAttribute itemscopeAttr)
        {
            this.node = node;
            this.itemscopeAttr = itemscopeAttr;

            nodeType = XmlNodeType.Element;
        }

        public Uri Href
        {
            get
            {
                var hrefAttr = this.node.GetAttribute("href");
                if (hrefAttr == null)
                {
                    return null;
                }
                Uri href;
                Uri.TryCreate(hrefAttr.Value, UriKind.RelativeOrAbsolute, out href);
                return href;
            }
        }

        public string Content
        {
            get
            {
                return this.node.InnerText;
            }
        }

        public Item Item
        {
            get
            {
                var itemscopeAttr = this.node.GetAttributeItemScope();
                if (itemscopeAttr != null)
                {
                    return new Item(this.node);
                }
                return null;
            }
        }

        public string Value
        {
            get
            {
                if (this.nodeType != XmlNodeType.Text)
                {
                    return "";
                }

                var item = this.Item;
                if(item != null) { return item.ToString(); }

                var href = this.Href;
                if (href != null) { return href.OriginalString; }

                return this.Content;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Item == null; // && Href == null && String.IsNullOrWhiteSpace(Content);
            }
        }

        public string Name
        {
            get
            {
                if (nodeType == XmlNodeType.Text)
                    return "";
                return itemscopeAttr.Value;
            }
        }

        public string NamespaceURI
        {
            get { return String.Empty; }
        }

        XmlNodeType nodeType;
        public System.Xml.XmlNodeType NodeType
        {
            get
            {
                return nodeType;
            }
        }

        public int Depth
        {
            get
            {
                if (nodeType == XmlNodeType.Text)
                    return 1;

                return 0;
            }
        }

        public bool Read(out IMicrodataNode childItem)
        {
            if (nodeType == XmlNodeType.Element)
            {
                if (this.Item != null)
                {
                    nodeType = XmlNodeType.EndElement;
                    childItem = this.Item;
                    return true;
                }
                this.nodeType = XmlNodeType.Text;
                childItem = null;
                return true;
            }

            if(nodeType == XmlNodeType.Text)
            {
                nodeType = XmlNodeType.EndElement;
                childItem = null;
                return true;
            }

            childItem = null;
            return false;
        }
    }
}
