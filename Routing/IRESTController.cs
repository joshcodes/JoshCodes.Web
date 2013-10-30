using System;

using JoshCodes.Web.Models.Api;

namespace JoshCodes.Web.Routing
{
    public interface IRESTController<T>
    {
        T Get(WebId Id, out DateTime lastModified);
        void Put(WebId id, T entity, out DateTime lastModified, out Uri location);
        WebId Post(T entity, out DateTime lastModified);
    }
}
