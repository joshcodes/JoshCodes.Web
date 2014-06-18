using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace JoshCodes.Web.Microdata
{
    public class XmlMicrodataReader : XmlReader
    {

        private ReadState readState;
        private Item rootEntity;

        public XmlMicrodataReader(HtmlAgilityPack.HtmlNode rootEntity)
        {
            this.readState = System.Xml.ReadState.Initial;
            this.rootEntity = new Item(rootEntity);
            this.nodeType = XmlNodeType.None;
        }

        private List<IMicrodataNode> itemStack = new List<IMicrodataNode>();

        private string baseURI;
        public override string BaseURI
        {
            get { return baseURI; }
        }

        public override int Depth
        {
            get
            {
                if (itemStack.Count == 0)
                    return 0;

                return (itemStack.Count - 1) + itemStack.First().Depth;
            }
        }

        public override bool EOF
        {
            get
            {
                return readState == System.Xml.ReadState.EndOfFile;
            }
        }

        public override bool IsEmptyElement
        {
            get { return false; }
        }

        public override string LocalName
        {
            get
            {
                if (readState == System.Xml.ReadState.Initial ||
                    nodeType == XmlNodeType.None)
                    return "";

                if (nodeType == XmlNodeType.XmlDeclaration)
                    return "xml";

                return itemStack.First().Name;
            }
        }

        public override string NamespaceURI
        {
            get { return ""; } // itemStack.First().NamespaceURI; }
        }

        private XmlNodeType nodeType;
        public override XmlNodeType NodeType
        {
            get
            {
                return nodeType;
            }
        }

        private void Push(IMicrodataNode node)
        {
            itemStack.Insert(0, node);
        }

        private void Pop()
        {
            itemStack.RemoveAt(0);
        }

        public override bool Read()
        {
            if(readState == System.Xml.ReadState.Initial)
            {
                nodeType = XmlNodeType.XmlDeclaration;
                readState = System.Xml.ReadState.Interactive;
                return true;
            }

            if(nodeType == XmlNodeType.XmlDeclaration)
            {
                Push(rootEntity);
                nodeType = rootEntity.NodeType;
                return true;
            }

            while(itemStack.Count > 0)
            {
                var item = itemStack.First();
                IMicrodataNode childItem;
                if(item.Read(out childItem))
                {
                    if (childItem != null)
                    {
                        Push(childItem);
                        nodeType = childItem.NodeType;
                    }
                    else
                        nodeType = item.NodeType;
                    return true;
                }
                Pop();
            }
            readState = System.Xml.ReadState.EndOfFile;
            nodeType = XmlNodeType.None;
            return false;
        }

        public override ReadState ReadState
        {
            get { return readState; }
        }

        public override void ResolveEntity()
        {
        }

        public override string Value
        {
            get
            {
                if (nodeType == XmlNodeType.XmlDeclaration)
                    return "version=\"1.0\"";

                if (itemStack.Count == 0)
                    return "";

                return itemStack.First().Value;
            }
        }

        #region Namespaces

        public override string LookupNamespace(string prefix)
        {
            return prefix;
        }

        public override string Prefix
        {
            get
            {
                //When overridden in a derived class, gets the namespace prefix associated with the current node.
                return null;
            }
        }

        public override XmlNameTable NameTable
        {
            get { return null; }
        }

        #endregion

        #region Attributes

        public override int AttributeCount
        {
            get { return 0; }
        }

        public override string GetAttribute(int i)
        {
            return null;
        }

        public override string GetAttribute(string name, string namespaceURI)
        {
            return null;
        }

        public override string GetAttribute(string name)
        {
            return null;
        }
        public override bool MoveToAttribute(string name, string ns)
        {
            return false;
        }

        public override bool MoveToAttribute(string name)
        {
            return false;
        }

        public override bool MoveToFirstAttribute()
        {
            return false;
        }

        public override bool MoveToNextAttribute()
        {
            return false;
        }
        public override bool MoveToElement()
        {
            // Return Value true if the reader is positioned on an attribute (the reader moves to the element that owns the attribute);
            // false if the reader is not positioned on an attribute (the position of the reader does not change).
            return false; // There are no attributes in Microdata
        }

        public override bool ReadAttributeValue()
        {
            return false;
        }

        #endregion

    }
}
