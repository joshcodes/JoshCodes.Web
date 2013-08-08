using System;
using System.Security.Authentication;
using System.Web.Mvc;

using JoshCodes.Web.Routing.Extensions;

namespace JoshCodes.Web.Attributes
{
    public class RestrictedAttribute : ActionFilterAttribute
    {
        public enum Permissions
        {
            Grant, View
        }

        private readonly string token;

        public RestrictedAttribute(Permissions permission, string token)
        {
            this.token = token;
        }

        public string Token { get { return token; } }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            /* TODO: this
            var tempData = filterContext.Controller.TempData;
            if(!tempData.HasPermissions(token))
            {
                if (!tempData.HasUserAuthenticated())
                {
                    //send them off to the login page
                    var url = new UrlHelper(filterContext.RequestContext);
                    var loginUrl = url.UrlFor<LandingController>((c) => c.Login());
                    filterContext.HttpContext.Response.Redirect(loginUrl.ToString(), true);
                }
                else
                {
                    // Send the user to something they do have access to
                    // TODO: Actually succeed as re-routing them
                }
            }*/
        }
    }
}

