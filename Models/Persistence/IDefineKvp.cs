using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshCodes.Web.Models.Persistence
{
    public interface IDefineKvp : IDefineModelObject
    {
        string Container { get; }
        string LookupKey { get; }
        string Value { get; }
    }
}
