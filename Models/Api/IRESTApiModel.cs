using System;

using JoshCodes.Web.Routing;
using System.Runtime.Serialization;

namespace JoshCodes.Web.Models.Api
{
    public interface IRESTApiModel
    {
        [DataMember(Name="id")]
        WebId Id { get; }

        [DataMember(Name = "last_modified")]
        DateTime LastModified { get; }

        void ResolveLinks(IUriHelper urlHelper);
    }
}
