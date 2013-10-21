using System;
using System.Linq;
using System.Linq.Expressions;

using JoshCodes.Web.Routing.Extensions;
using JoshCodes.Web.Models.Api;

namespace JoshCodes.Web.Routing
{
    public class UriHelper : IUriHelper
    {
        private System.Web.Mvc.UrlHelper mvcUrlHelper;
        private System.Web.Mvc.Controller controller;

        public UriHelper(System.Web.Mvc.Controller controller)
        {
            this.mvcUrlHelper = controller.Url;
            this.controller = controller;
        }

        #region IUrlHelper implementation
        public System.Uri UrlFor<TController> (Expression<Action<TController>> link, bool fullUrl)
            where TController : System.Web.Mvc.IController
        {
            return mvcUrlHelper.UrlFor<TController>(link, fullUrl);
        }

		public System.Uri UrlFor<TController> (Expression<Func<TController, System.Web.Mvc.ActionResult>> link, bool fullUrl)
            where TController : System.Web.Mvc.IController
        {
            return mvcUrlHelper.UrlFor<TController>(link, fullUrl);
        }

        public string Action<TController> (Expression<Action<TController>> link, bool fullUrl)
            where TController : System.Web.Mvc.IController
        {
            return mvcUrlHelper.Action<TController>(link, fullUrl);
        }

        public string Action<TController> (Expression<Func<TController, System.Web.Mvc.ActionResult>> link, bool fullUrl)
            where TController : System.Web.Mvc.IController
        {
            return mvcUrlHelper.Action<TController>(link, fullUrl);
        }

		public System.Uri CurrentUri {
            get {
                return mvcUrlHelper.RequestContext.HttpContext.Request.Url;
            }
        }

        public string Action(System.Reflection.MethodInfo method, bool fullUrl, ParameterLookupDelegate resolveParams)
        {
            return mvcUrlHelper.Action(method, fullUrl, resolveParams);
        }
        #endregion

        internal static Uri RestfulUrlFor<TId>(Type controllerType, TId id, bool fullUrl, System.Web.Mvc.UrlHelper mvcUrlHelper)
        {
            // Get initial route values for controller / action
            var routeValues = new System.Web.Routing.RouteValueDictionary();
            var controllerTypeName = controllerType.Name;
            var controllerName = controllerTypeName.Remove(controllerTypeName.Length - "Controller".Length);
            routeValues.Add("controller", controllerName);
            routeValues.Add("action", "Index");
            if (id != null)
            {
                routeValues.Add("self", id);
            }

            var relativeUrlStr = mvcUrlHelper.Action("Index", routeValues);
            var relativeUrl = new System.Uri(relativeUrlStr, UriKind.Relative);
            if (!fullUrl)
            {
                return relativeUrl;
            }
            var baseUrl = mvcUrlHelper.RequestContext.HttpContext.Request.Url;
            return new System.Uri(baseUrl, relativeUrl);
        }

        public Uri RestfulUrlForUrn(Uri idUrn, bool fullUrl = false)
        {
            // Get initial route values for controller / action
            var controllerType = this.controller.GetType();
            return RestfulUrlFor<Uri>(controllerType, idUrn, fullUrl, mvcUrlHelper);
        }

        public Uri RestfulUrlFor<TEntity, TId>(TId id, bool fullUrl = false)
        {
            // Get initial route values for controller / action
            var controllerType = typeof(TEntity);
            return RestfulUrlFor<TId>(controllerType, id, fullUrl, mvcUrlHelper);
        }

        public Uri RestfulUrlFor<TController>(System.Collections.Generic.IDictionary<string, string> queryParams, bool fullUrl = false)
        {
            var baseUri = RestfulUrlFor<TController, string>(null, fullUrl);
            var queryStringCollection = System.Web.HttpUtility.ParseQueryString(baseUri.IsAbsoluteUri ? baseUri.Query : string.Empty);

            foreach (var queryParamKvp in queryParams)
            {
                queryStringCollection[queryParamKvp.Key] = queryParamKvp.Value;
            }

            string uri = baseUri.IsAbsoluteUri ?
                baseUri.AbsoluteUri.Substring(0, baseUri.AbsoluteUri.Length - baseUri.Query.Length) :
                baseUri.ToString();

            return new Uri(uri + "?" + queryStringCollection.ToString(), baseUri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
        }


        public Uri RestfulUrlFor<TController, TApiModel, T>(Expression<Func<TApiModel, T>> parameter, string value, bool fullUrl = false)
            where TController : IRESTController<TApiModel>
        {
            var parameterExprBody = (MemberExpression)parameter.Body;
            var propInfo = parameterExprBody.Member;
            var propName = propInfo.Name;
            var dataMemberAttr = (System.Runtime.Serialization.DataMemberAttribute)propInfo.GetCustomAttributes(
                typeof(System.Runtime.Serialization.DataMemberAttribute), false).FirstOrDefault();
            if (dataMemberAttr != null)
            {
                propName = dataMemberAttr.Name;
            }

            var queryParams = new System.Collections.Generic.Dictionary<string, string>() { { propName, value } };
            return this.RestfulUrlFor<TController>(queryParams, fullUrl);
        }

        public System.Uri RestfulUrlFor<TController, TApiModel>(Expression<Func<TApiModel, WebId>> parameter, WebId value, bool fullUrl = false)
            where TController : IRESTController<TApiModel>
        {
            var parameterExprBody = (MemberExpression)parameter.Body;
            var propInfo = parameterExprBody.Member;
            var propName = propInfo.Name;
            var dataMemberAttr = (System.Runtime.Serialization.DataMemberAttribute)propInfo.GetCustomAttributes(
                typeof(System.Runtime.Serialization.DataMemberAttribute), false).FirstOrDefault();
            if (dataMemberAttr != null)
            {
                propName = dataMemberAttr.Name;
            }

            var queryParams = new System.Collections.Generic.Dictionary<string, string>()
            {
                { propName + ".key", value.Key },
                { propName + ".guid", value.Guid.ToString() },
                { propName + ".urn", value.Urn.AbsoluteUri },
                { propName + ".source", value.Source.OriginalString }
            };
            return this.RestfulUrlFor<TController>(queryParams, fullUrl);
        }

        public Uri RestfulUrlFor<TController, TApiModel, T>(Func<TApiModel> parameters, bool fullUrl = false)
            where TController : IRESTController<TApiModel>
        {
            throw new NotImplementedException();
        }

        public Uri RestfulUrlFor<TController>(Models.Domain.DomainId id, bool fullUrl = false)
        {
            var baseUri = RestfulUrlFor<TController, string>(id == null? String.Empty : id.Urn.AbsoluteUri, fullUrl);
            return baseUri;
        }
    }
}

