using System;
using System.Collections.Generic;

namespace JoshCodes.Web.Models.Persistence
{
    public interface IStoreObjects<TDefinition>
    {
        TDefinition Find(string key);

        TDefinition Find(Guid key);

        TDefinition Find(Uri urn);

        TDefinition Find(Domain.DomainId id);

        IEnumerable<TDefinition> All();
    }
}
