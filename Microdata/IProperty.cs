using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshCodes.Web.Microdata
{
    public interface IProperty
    {
        Uri Href { get; }

        string Content { get; }

        public IItem Item { get;}

        object Value { get; }
    }
}
