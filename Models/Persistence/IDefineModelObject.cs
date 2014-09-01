using System;

namespace JoshCodes.Web.Models.Persistence
{
    public interface IDefineModelObject
    {
        Guid Key { get; }

        DateTimeOffset LastModified { get; set; }

        string[] UrnNamespace { get; }
    }
}
