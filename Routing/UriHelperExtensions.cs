using System;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace JoshCodes.Web.Routing.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string GetAction(this System.Web.Mvc.UrlHelper urlHelper)
        {
            return (string)urlHelper.RequestContext.RouteData.Values["action"];
        }

        public static string Action<TController>(this System.Web.Mvc.UrlHelper urlHelper, Expression<Action<TController>> link, bool fullUrl = false)
            where TController : IController
        {
            var methodExpression = link.Body as MethodCallExpression;
            return Action(urlHelper, methodExpression, fullUrl);
        }

        public static string Action<TController>(this System.Web.Mvc.UrlHelper urlHelper, Expression<Func<TController, ActionResult>> link, bool fullUrl = false)
            where TController : IController
        {
            var methodExpression = link.Body as MethodCallExpression;
            return Action(urlHelper, methodExpression, fullUrl);
        }

        public static string Action(this System.Web.Mvc.UrlHelper urlHelper, System.Reflection.MethodInfo method, bool fullUrl = false, ParameterLookupDelegate resolveParam = null)
        {
            // Get initial route values for controller / action
            var routeValues = method.GetRouteValues(resolveParam);
            var action = (string) routeValues["action"];

            return Action(urlHelper, action, routeValues, fullUrl);
        }

        private static string Action(System.Web.Mvc.UrlHelper urlHelper, MethodCallExpression methodExpression, bool fullUrl)
        {
            // Get initial route values for controller / action
            var routeValues = methodExpression.GetRouteValues();
            var action = (string) routeValues["action"];

            return Action(urlHelper, action, routeValues, fullUrl);
        }

        private static string Action(System.Web.Mvc.UrlHelper urlHelper, string action, System.Web.Routing.RouteValueDictionary routeValues, bool fullUrl)
        {
            var url = urlHelper.Action(action, routeValues);
            if(fullUrl)
            {
				var relativeUrl = new System.Uri(url, UriKind.Relative);
                var baseUrl = urlHelper.RequestContext.HttpContext.Request.Url;
				url = new System.Uri(baseUrl, relativeUrl).ToString();
            }

            return url;
        }

		public static System.Uri UrlFor<TController>(this System.Web.Mvc.UrlHelper urlHelper, Expression<Action<TController>> link, bool fullUrl = false) where TController : IController
        {
            var methodExpression = link.Body as MethodCallExpression;
            return UrlFor(urlHelper, methodExpression, fullUrl);
        }

		public static System.Uri UrlFor<TController>(this System.Web.Mvc.UrlHelper urlHelper, Expression<Func<TController, ActionResult>> link, bool fullUrl = false) where TController : IController
        {
            var methodExpression = link.Body as MethodCallExpression;
            return UrlFor(urlHelper, methodExpression, fullUrl);
        }

		private static System.Uri UrlFor(System.Web.Mvc.UrlHelper urlHelper, MethodCallExpression methodExpression, bool fullUrl)
        {
            // Get initial route values for controller / action
            var routeValues = methodExpression.GetRouteValues();
            var action = (string) routeValues["action"];

            var relativeUrlStr = urlHelper.Action( action, routeValues);
			var relativeUrl = new System.Uri(relativeUrlStr, UriKind.Relative);
            if(!fullUrl)
            {
                return relativeUrl;
            }
            var baseUrl = urlHelper.RequestContext.HttpContext.Request.Url;
			return new System.Uri(baseUrl, relativeUrl);
        }
    }
}

