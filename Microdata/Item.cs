using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JoshCodes.Web.Microdata
{
    class Item : IMicrodataNode
    {
        private HtmlAgilityPack.HtmlNode node;

        public Item(HtmlAgilityPack.HtmlNode node)
        {
            this.node = node;

            var typeAttr = node.GetAttributeItemType();
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

        private IEnumerator<IMicrodataNode> properties;

        private IEnumerable<IMicrodataNode> GetProperties(HtmlAgilityPack.HtmlNode node)
        {
            foreach (var childNode in node.ChildNodes)
            {
                foreach (var property in GetPropertiesRecursive(childNode))
                {
                    yield return property;
                }
            }
        }

        private IEnumerable<IMicrodataNode> GetPropertiesRecursive(HtmlAgilityPack.HtmlNode node)
        {
            var itempropAttr = node.GetAttributeItemProperty();
            if (itempropAttr != null)
            {
                // If this has an itemscope then return an item, otherwise a property
                // This is because item iterates appropriately for the xml expected
                var itemscopeAttr = node.GetAttributeItemScope();
                yield return (itemscopeAttr != null)?
                     (IMicrodataNode)new Item(node) :
                     (IMicrodataNode)new Property(node, itempropAttr);
                yield break;
            }
            foreach (var childNode in node.ChildNodes)
            {
                foreach (var property in GetPropertiesRecursive(childNode))
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
                var attrProp = this.node.GetAttributeItemProperty();
                if (attrProp != null)
                    return attrProp.Value;

                var splits = this.Type.OriginalString.Split(new char[] { '/' });
                return splits[splits.Length - 1];
            }
        }

        public string NamespaceURI
        {
            get { return this.Type.OriginalString; }
        }

        XmlNodeType nodeType = XmlNodeType.Element;
        public System.Xml.XmlNodeType NodeType
        {
            get { return nodeType; }
        }

        public int Depth
        {
            get
            {
                return 0;
            }
        }

        public bool Read(out IMicrodataNode childItem)
        {
            if (!properties.MoveNext())
            {
                childItem = null;
                if(nodeType == XmlNodeType.Element)
                {
                    nodeType = XmlNodeType.EndElement;
                    return true;
                }
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
