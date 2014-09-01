
namespace JoshCodes.Web.Routing
{
    /*
    class HttpUriHelper : IUriHelper<System.Web.Http.Controllers.IHttpController>
    {
        private System.Web.Http.Routing.UrlHelper routingUrlHelper;

        public HttpUriHelper(System.Web.Http.Routing.UrlHelper routingUrlHelper)
        {
            this.routingUrlHelper = routingUrlHelper;
        }

        public Uri CurrentUri
        {
            get
            {
                return routingUrlHelper.Request.RequestUri;
            }
        }

        public Uri UrlFor(Expression<Action<System.Web.Http.Controllers.IHttpController>> link, bool fullUrl = false)
        {
            var routeValues = ((MethodCallExpression)link.Body).GetRouteValues();
            var uriString = routingUrlHelper.Link(link.Name, routeValues);
            return new Uri(uriString);
        }

        public Uri UrlFor(Expression<Func<System.Web.Http.Controllers.IHttpController, System.Web.Mvc.ActionResult>> link, bool fullUrl = false)
        {
            var methodExpression = link.Body as MethodCallExpression;
            var routeValues = methodExpression.GetRouteValues();
            var uriString = routingUrlHelper.Link(link.Name, routeValues);
            return new Uri(uriString);
        }

        public string Action(Expression<Action<System.Web.Http.Controllers.IHttpController>> link, bool fullUrl = false)
        {
            var methodExpression = link.Body as MethodCallExpression;
            var routeValues = methodExpression.GetRouteValues();
            var uriString = routingUrlHelper.Link(link.Name, routeValues);
            return uriString;
        }

        public string Action(Expression<Func<System.Web.Http.Controllers.IHttpController, System.Web.Mvc.ActionResult>> link, bool fullUrl = false)
        {
            var methodExpression = link.Body as MethodCallExpression;
            var routeValues = methodExpression.GetRouteValues();
            var uriString = routingUrlHelper.Link(link.Name, routeValues);
            return uriString;
        }

        public string Action(System.Reflection.MethodInfo method, bool fullUrl = false, ParameterLookupDelegate resolveParams = null)
        {
            // Get initial route values for controller / action
            var routeValues = method.GetRouteValues(resolveParams);
            var action = (string)routeValues["action"];
            var uriString = routingUrlHelper.Link(action, routeValues);
            return uriString;
        }
    }*/
}
