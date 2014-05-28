using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Http;
using System.Web.Http.Routing;

using JoshCodes.Web.Routing.Extensions;

namespace JoshCodes.Web.Routing
{
    public static class ApiUrlHelper
    {
        public static Uri Route<TController>(this UrlHelper urlHelper, Expression<Action<TController>> link, bool fullUrl = false)
            where TController : ApiController
        {
            var methodExpression = link.Body as MethodCallExpression;
            return Action(urlHelper, methodExpression, fullUrl);
        }

        public static Uri Route<TController, TResult>(this UrlHelper urlHelper, Expression<Func<TController, IEnumerable<TResult>>> link, bool fullUrl = false)
            where TController : ApiController
        {
            var methodExpression = link.Body as MethodCallExpression;
            return Action(urlHelper, methodExpression, fullUrl);
        }

        public static Uri Action(UrlHelper urlHelper, System.Reflection.MethodInfo method, bool fullUrl = false, ParameterLookupDelegate resolveParam = null)
        {
            // Get initial route values for controller / action
            var routeValues = method.GetRouteValues(resolveParam);

            return Action(urlHelper, routeValues, fullUrl);
        }

        private static Uri Action(UrlHelper urlHelper, MethodCallExpression methodExpression, bool fullUrl)
        {
            // Get initial route values for controller / action
            var routeValues = methodExpression.GetRouteValues();

            return Action(urlHelper, routeValues, fullUrl);
        }

        private static Uri Action(UrlHelper urlHelper, System.Web.Routing.RouteValueDictionary routeValues, bool fullUrl)
        {
            var configuration = urlHelper.GetHttpConfiguration();
            var defaultable = configuration.Routes.ContainsKey("DefaultApi");
            foreach(var route in configuration.Routes)
            {
                route.Defaults.GetType();
            }
            var url = urlHelper.Route("DefaultApi", routeValues);
            if(fullUrl)
            {
				var relativeUrl = new System.Uri(url, UriKind.Relative);
                var baseUrl = urlHelper.Request.RequestUri;
				return new System.Uri(baseUrl, relativeUrl);
            }

            return new Uri(url, UriKind.RelativeOrAbsolute);
        }

        private static System.Web.Http.HttpConfiguration GetHttpConfiguration(this UrlHelper urlHelper)
        {
            var request = urlHelper.Request;
            foreach(var propertyKvp in request.Properties)
            {
                var property = propertyKvp.Value;
                if(typeof(System.Web.Http.HttpConfiguration).IsInstanceOfType(property))
                {
                    return property as System.Web.Http.HttpConfiguration;
                }
            }
            throw new Exception("Route data not found");
        }
    }
}
