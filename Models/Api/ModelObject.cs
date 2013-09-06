using System;

using JoshCodes.Web.Models.Domain;
using JoshCodes.Web.Models.Persistence;

namespace JoshCodes.Web.Models.Api
{
    public class ModelObject
    {
        protected ModelObject(WebId id)
        {
            this.Id = id;
        }

        protected ModelObject(string key, Guid guid, Uri urn, Uri url)
        {
            this.Id = new WebId(key, guid, urn, url);
        }

        public WebId Id { get; private set; }
    }
}
