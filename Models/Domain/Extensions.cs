using System;
using System.Collections.Generic;

using JoshCodes.Web.Models.Persistence;

namespace JoshCodes.Web.Models.Domain
{
    public static class Extensions
    {
        public static DomainId ToDomainId(this Api.WebId webId)
        {
            if (webId == null)
            {
                return null;
            }
            return new DomainId(webId.Key, webId.Guid, webId.Urn);
        }

        public static bool IsSame<T>(this ModelObject<T> modelObject1, ModelObject<T> modelObject2)
            where T : IDefineModelObject
        {
            var modelId1 = modelObject1.Id;
            var modelId2 = modelObject2.Id;
            return (modelId1 != default(Guid) && modelId1 != Guid.Empty && modelId1 == modelId2);
        }
    }
}
