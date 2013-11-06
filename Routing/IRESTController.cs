using System;

using JoshCodes.Web.Models.Api;

namespace JoshCodes.Web.Routing
{
    public interface IRESTController<TEntity>
    {
        TEntity Get(WebId Id, out DateTime lastModified);
        void Put(TEntity entity, out DateTime lastModified);
        WebId Post(TEntity entity, out DateTime lastModified);
    }
}
