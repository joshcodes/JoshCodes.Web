using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoshCodes.Web.Routing
{
    public interface IRESTController<T>
    {
        T Get(Uri Id);
        void Put(Uri id, T entity, out DateTime lastModified);
        Uri Post(T entity, out DateTime lastModified);
    }
}
