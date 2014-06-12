using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace JoshCodes.Web.Microdata
{
    public class XmlMicrodataReader : XmlReader
    {
        internal const string ItemScope = "itemscope";
        internal const string ItemType = "itemtype";
        internal const string ItemProp = "itemprop";

        private HtmlAgilityPack.HtmlDocument htmlDoc;
        private ReadState readState;

        public XmlMicrodataReader(Stream stream, Uri from)
        {
            htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.Load(stream);
            this.baseURI = from == null? null : from.OriginalString;
            this.readState = System.Xml.ReadState.Initial;
        }


        private List<IMicrodataNode> itemStack = new List<IMicrodataNode>();

        private string baseURI;
        public override string BaseURI
        {
            get { return baseURI; }
        }

        public override int Depth
        {
            get { return itemStack.Count; }
        }

        public override bool EOF
        {
            get
            {
                return readState != System.Xml.ReadState.EndOfFile;
            }
        }

        public override bool IsEmptyElement
        {
            get { return itemStack.First().IsEmpty; }
        }

        public override string LocalName
        {
            get { return itemStack.First().Name; }
        }
        public override string NamespaceURI
        {
            get { return itemStack.First().NamespaceURI; }
        }

        public override XmlNodeType NodeType
        {
            get
            {
                if(itemStack.Count == 0)
                {
                    return XmlNodeType.Document;
                }
                return itemStack.First().NodeType;
            }
        }

        public override bool Read()
        {
            if(readState == System.Xml.ReadState.Initial)
            {
                var root = new Items(htmlDoc.DocumentNode);
                itemStack.Insert(0, root);
                readState = System.Xml.ReadState.Interactive;
            }

            while(itemStack.Count > 0)
            {
                var item = itemStack.First();
                IMicrodataNode childItem;
                if(item.Read(out childItem))
                {
                    if(childItem != null)
                    {
                        itemStack.Insert(0, childItem);
                    }
                    return true;
                }
                itemStack.Remove(item);
            }
            readState = System.Xml.ReadState.EndOfFile;
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
            get { return itemStack.First().Value; }
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
