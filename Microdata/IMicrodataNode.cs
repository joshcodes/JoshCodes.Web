
namespace JoshCodes.Web.Microdata
{
    interface IMicrodataNode
    {
        bool IsEmpty { get; }

        string Name { get; }

        System.Xml.XmlNodeType NodeType { get; }

        string Value { get; }

        string NamespaceURI { get; }

        // Depth added by this node internally
        // (this is not the depth of the node itself).
        int Depth { get; }

        bool Read(out IMicrodataNode childItem);
    }
}
