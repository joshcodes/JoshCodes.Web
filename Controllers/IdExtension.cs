using System;
using System.Collections.Generic;

using JoshCodes.Web.Routing;
using JoshCodes.Web.Routing.Extensions;
using JoshCodes.Web.Models.Api;

namespace JoshCodes.Web.Controllers
{
    public static class IdExtension
    {
        public static Models.Api.WebId ConvertToWebId<TController, TApiModel>(this System.Web.Mvc.UrlHelper mvcUrlHelper, Models.Domain.DomainId id)
            where TController : System.Web.Mvc.Controller, IRESTController<TApiModel>
            where TApiModel : IRESTApiModel
        {
            var urlHelper = new UriHelper(mvcUrlHelper);
            return IdExtension.ConvertToWebId<TController, TApiModel>(urlHelper, id);
        }

        public static Models.Api.WebId ConvertToWebId<TController, TApiModel>(this IUriHelper urlHelper, Models.Domain.DomainId id)
            where TController : System.Web.Mvc.Controller, IRESTController<TApiModel>
            where TApiModel : IRESTApiModel
        {
            if (id == null)
            {
                return null;
            }
            var source = urlHelper.RestfulUrlFor<TController, TApiModel>((model) => model.Id, id, true);
            return new Models.Api.WebId(id.Key, id.Guid, id.Urn, source);
        }

        public static Models.Api.WebId ConvertToWebId<TController, TApiModel>(this System.Web.Mvc.UrlHelper mvcUrlHelper, string key, Guid guid, Uri urn)
            where TController : System.Web.Mvc.Controller, IRESTController<TApiModel>
            where TApiModel : IRESTApiModel
        {
            var domainId = new Models.Domain.DomainId(key, guid, urn);
            return ConvertToWebId<TController, TApiModel>(mvcUrlHelper, domainId);
        }
    }
}
