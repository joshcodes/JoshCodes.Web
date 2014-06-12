using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshCodes.Web.Microdata
{
    interface IMicrodataNode
    {
        bool IsEmpty { get; }

        string Name { get; }

        System.Xml.XmlNodeType NodeType { get; }

        bool Read(out IMicrodataNode childItem);

        string Value { get; }
    }
}
