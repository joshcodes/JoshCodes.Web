using System;

using JoshCodes.Web.Models.Api;

namespace JoshCodes.Web.Routing
{
    public interface IRESTController<T>
    {
        T Get(Uri Id, out DateTime lastModified);
        void Put(Uri id, T entity, out DateTime lastModified);
        WebId Post(T entity, out DateTime lastModified);
    }
}
