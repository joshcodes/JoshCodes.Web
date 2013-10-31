using System;

namespace JoshCodes.Web.Models.Persistence
{
    public interface IDefineModelObject
    {
        Guid Key { get; }

        DateTime LastModified { get; set; }

        string[] UrnNamespace { get; }
    }
}
