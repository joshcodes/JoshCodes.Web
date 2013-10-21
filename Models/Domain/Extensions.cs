using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoshCodes.Web.Models.Domain
{
    public static class Extensions
    {
        public static DomainId GetDomainId(this Persistence.IDefineModelObject modelObject)
        {
            if (modelObject == null)
            {
                return null;
            }
            return new DomainId(modelObject.IdKey, modelObject.IdGuid, modelObject.IdUrn);
        }

        public static DomainId ToDomainId(this Api.WebId webId)
        {
            if (webId == null)
            {
                return null;
            }
            return new DomainId(webId.Key, webId.Guid, webId.Urn);
        }
    }
}
