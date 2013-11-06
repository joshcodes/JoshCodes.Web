using System;

using JoshCodes.Web.Routing;

namespace JoshCodes.Web.Models.Api
{
    public interface IRESTApiModel
    {
        WebId Id { get; }

        DateTime LastModified { get; }

        void ResolveLinks(IUriHelper urlHelper);
    }
}
