using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshCodes.Web.Microdata
{
    class Item : IMicrodataNode
    {
        public Item(HtmlAgilityPack.HtmlNode node)
        {
            var typeAttr = node.Attributes.FirstOrDefault((attr) =>
                {
                    var attrName = attr.Name;
                    var isItemTypeAttr = String.Compare(attr.Name, XmlMicrodataReader.ItemType, true) == 0;
                    return isItemTypeAttr;
                });
            if(typeAttr != null)
            {
                var typeString = typeAttr.Value;
                Uri typeUri;
                Uri.TryCreate(typeString, UriKind.Absolute, out typeUri);
                this.Type = typeUri;
            }

            this.properties = GetProperties(node).GetEnumerator();
        }

        public Uri Type { get; private set; }

        private IEnumerator<Property> properties;

        private IEnumerable<Property> GetProperties(HtmlAgilityPack.HtmlNode node)
        {
            var itemscopeAttr = node.Attributes.FirstOrDefault((attr) => String.Compare(attr.Name, XmlMicrodataReader.ItemProp, true) == 0);
            if (itemscopeAttr != null)
            {
                yield return new Property(node, itemscopeAttr);
                yield break;
            }
            foreach (var childNode in node.Descendants())
            {
                foreach (var property in GetProperties(childNode))
                {
                    yield return property;
                }
            }
        }

        public bool IsEmpty
        {
            get { return false; }
        }

        public string Name
        {
            get
            {
                var splits = this.Type.OriginalString.Split(new char[] { '/' });
                return splits[splits.Length - 1];
            }
        }

        public string NamespaceURI
        {
            get { return this.Type.OriginalString; }
        }

        public System.Xml.XmlNodeType NodeType
        {
            get { return System.Xml.XmlNodeType.Element; }
        }

        public bool Read(out IMicrodataNode childItem)
        {
            if (!properties.MoveNext())
            {
                childItem = null;
                return false;
            }

            childItem = properties.Current;
            return true;
        }

        public string Value
        {
            get { return String.Empty; }
        }
    }
}
