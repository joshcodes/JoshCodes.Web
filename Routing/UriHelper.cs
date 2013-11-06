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

        public UriHelper(System.Web.Mvc.Controller controller)
        {
            this.mvcUrlHelper = controller.Url;
        }

        public UriHelper(System.Web.Mvc.UrlHelper mvcUrlHelper)
        {
            this.mvcUrlHelper = mvcUrlHelper;
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

        public Uri RestfulUrlFor<TController, TId>(TId id, bool fullUrl = false)
        {
            // Get initial route values for controller / action
            var controllerType = typeof(TController);
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

        public System.Uri RestfulUrlFor<TController, TApiModel>(
            Expression<Func<TApiModel, WebId>> parameter,
            JoshCodes.Web.Models.Domain.DomainId value,
            bool fullUrl = false)
                where TController : IRESTController<TApiModel>
                where TApiModel : IRESTApiModel
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

            var queryParams = new System.Collections.Generic.Dictionary<string, string>();
            if (value == null)
            {
                return null;
            }

            if (value.Guid != default(Guid))
            {
                queryParams.Add(propName + ".guid", value.Guid.ToString());
            }
            else if (!String.IsNullOrWhiteSpace(value.Key))
            {
                queryParams.Add(propName + ".key", value.Key);
            }
            else if (value.Urn != null)
            {
                queryParams.Add(propName + ".urn", value.Urn.AbsoluteUri);
            }
            else
            {
                queryParams.Add(propName + ".key", null);
            }
            return this.RestfulUrlFor<TController>(queryParams, fullUrl);
        }

        public System.Uri RestfulUrlFor<TController, TApiModel>(Expression<Func<TApiModel, WebId>> parameter, WebId value, bool fullUrl = false)
            where TController : IRESTController<TApiModel>
            where TApiModel : IRESTApiModel
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

            var queryParams = new System.Collections.Generic.Dictionary<string, string>();
            if(value.Guid != default(Guid))
            {
                queryParams.Add(propName + ".guid", value.Guid.ToString());
            }
            else if (!String.IsNullOrWhiteSpace(value.Key))
            {
                queryParams.Add(propName + ".key", value.Key);
            }
            else if (value.Urn != null)
            {
                queryParams.Add(propName + ".urn", value.Urn.AbsoluteUri);
            }
            else if (value.Source != null)
            {
                queryParams.Add(propName + ".source", value.Source.OriginalString);
            }
            else
            {
                queryParams.Add(propName + ".key", null);
            }
            return this.RestfulUrlFor<TController>(queryParams, fullUrl);
        }

        public Uri RestfulUrlFor<TController, TApiModel, T>(Func<TApiModel> parameters, bool fullUrl = false)
            where TController : IRESTController<TApiModel>
            where TApiModel : IRESTApiModel
        {
            throw new NotImplementedException();
        }

        public Uri RestfulUrlFor<TController>(Models.Domain.DomainId value, bool fullId = false, bool fullUrl = false)
        {
            string propName = "id";
            var queryParams = new System.Collections.Generic.Dictionary<string, string>();
            if (value.Guid != default(Guid))
            {
                queryParams.Add(propName + ".guid", value.Guid.ToString());
                if (!fullId)
                {
                    return this.RestfulUrlFor<TController>(queryParams, fullUrl);
                }
            }
            if (!String.IsNullOrWhiteSpace(value.Key))
            {
                queryParams.Add(propName + ".key", value.Key);
                if (!fullId)
                {
                    return this.RestfulUrlFor<TController>(queryParams, fullUrl);
                }
            }
            if (value.Urn != null)
            {
                queryParams.Add(propName + ".urn", value.Urn.AbsoluteUri);
                if (!fullId)
                {
                    return this.RestfulUrlFor<TController>(queryParams, fullUrl);
                }
            }
            return this.RestfulUrlFor<TController>(queryParams, fullUrl);
        }
    }
}

