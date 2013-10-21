using System;

namespace JoshCodes.Web.Models.Api
{
    public interface IRESTApiModel
    {
        WebId Id { get; }

        DateTime LastModified { get; }
    }
}
