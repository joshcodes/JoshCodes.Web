using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Reflection;
using System.Collections.Generic;
using JoshCodes.Web.Models.Api;

namespace JoshCodes.Web.Routing
{
    public delegate object ParameterLookupDelegate(string paramName, int paramIndex);

    public interface IUriHelper
    {
        System.Uri UrlFor<TController>(Expression<Action<TController>> link, bool fullUrl = false)
            where TController : IController;

		System.Uri UrlFor<TController>(Expression<Func<TController, ActionResult>> link, bool fullUrl = false)
            where TController : IController;

        string Action<TController>(Expression<Action<TController>> link, bool fullUrl = false)
            where TController : IController;

        string Action<TController>(Expression<Func<TController, ActionResult>> link, bool fullUrl = false)
            where TController : IController;

        System.Uri RestfulUrlFor<TController, TId>(TId id, bool fullUrl = false);

        System.Uri RestfulUrlFor<TController>(IDictionary<string, string> queryParams, bool fullUrl = false);

        System.Uri RestfulUrlFor<TController>(Models.Domain.DomainId id, bool fullUrl = false);

        System.Uri RestfulUrlFor<TController, TApiModel, T>(Expression<Func<TApiModel, T>> parameter, string value, bool fullUrl = false)
            where TController : IRESTController<TApiModel>;

        System.Uri RestfulUrlFor<TController, TApiModel>(Expression<Func<TApiModel, WebId>> parameter,
                JoshCodes.Web.Models.Domain.DomainId value, bool fullUrl = false)
            where TController : IRESTController<TApiModel>
            where TApiModel : IRESTApiModel;

        System.Uri RestfulUrlFor<TController, TApiModel>(Expression<Func<TApiModel, WebId>> parameter, WebId value, bool fullUrl = false)
            where TController : IRESTController<TApiModel>
            where TApiModel : IRESTApiModel;

        System.Uri RestfulUrlFor<TController, TApiModel, T>(Func<TApiModel> parameters, bool fullUrl = false)
            where TController : IRESTController<TApiModel>
            where TApiModel : IRESTApiModel;

        string Action(MethodInfo method, bool fullUrl = false, ParameterLookupDelegate resolveParams = null);

		System.Uri CurrentUri { get; }
    }
}

