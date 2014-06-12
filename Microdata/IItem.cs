using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshCodes.Web.Microdata
{
    public interface IItem
    {
        Uri Type { get; }

        IEnumerable<IProperty> Properties { get; }
    }
}
