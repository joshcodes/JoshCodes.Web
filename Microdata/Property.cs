using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshCodes.Web.Microdata
{
    class Property : IMicrodataNode
    {
        private HtmlAgilityPack.HtmlNode node;
        private HtmlAgilityPack.HtmlAttribute itemscopeAttr;

        public Property(HtmlAgilityPack.HtmlNode node, HtmlAgilityPack.HtmlAttribute itemscopeAttr)
        {
            this.node = node;
            this.itemscopeAttr = itemscopeAttr;
        }

        public Uri Href
        {
            get
            {
                var hrefAttr = this.node.Attributes.FirstOrDefault((attr) => String.Compare(attr.Name, "href", true) == 0);
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
                var itemscopeAttr = this.node.Attributes.FirstOrDefault((attr) => String.Compare(attr.Name, XmlMicrodataReader.ItemScope, true) == 0);
                if (itemscopeAttr != null)
                {
                    return new Item(this.node, this.Name);
                }
                return null;
            }
        }

        public object Value
        {
            get
            {
                var item = this.Item;
                if(item != null) { return item; }

                var href = this.Href;
                if (href != null) { return href; }

                return this.Content;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Item == null && Href == null && String.IsNullOrWhiteSpace(Content);
            }
        }

        public string Name
        {
            get { return itemscopeAttr.Value; }
        }

        public System.Xml.XmlNodeType NodeType
        {
            get { return System.Xml.XmlNodeType.Element; }
        }

        public bool Read(out IMicrodataNode childItem)
        {
            childItem = this.Item;
            return childItem == null;
        }

        string IMicrodataNode.Value
        {
            get { return this.Value.ToString(); }
        }
    }
}
