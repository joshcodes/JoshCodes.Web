using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Reflection;
using System.Collections.Generic;

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

        System.Uri RestfulUrlForUrn(Uri idUrn, bool fullUrl = false);

        System.Uri RestfulUrlFor<TController, TId>(TId id, bool fullUrl = false);

        System.Uri RestfulUrlFor<TController>(IDictionary<string, string> queryParams, bool fullUrl = false);

        string Action(MethodInfo method, bool fullUrl = false, ParameterLookupDelegate resolveParams = null);

		System.Uri CurrentUri { get; }
    }
}

