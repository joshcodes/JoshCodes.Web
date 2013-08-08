using System;

namespace JoshCodes.Web.Routing.Extensions
{
    public static class UrlControllerExtensions
    {
        public static IUriHelper GetUrlHelper(this System.Web.Mvc.Controller controller)
        {
            return new UriHelper(controller);
        }
    }
}

