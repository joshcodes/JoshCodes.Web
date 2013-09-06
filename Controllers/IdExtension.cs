using System;
using System.Collections.Generic;

using JoshCodes.Web.Routing;
using JoshCodes.Web.Routing.Extensions;

namespace JoshCodes.Web.Controllers
{
    public static class IdExtension
    {
        public static Models.Api.WebId ConvertToWebId<TController>(this System.Web.Mvc.UrlHelper mvcUrlHelper, Models.Domain.DomainId id)
            where TController : System.Web.Mvc.Controller
        {
            var source = UriHelper.RestfulUrlFor(typeof(TController), id.Urn, true, mvcUrlHelper);
            return new Models.Api.WebId(id.Key, id.Guid, id.Urn, source);
        }
    }
}
