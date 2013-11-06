using System;

namespace JoshCodes.Web.Models.Api
{
    public class ResourceNotFoundException : Exception, IAPIException
    {
        public string Reason
        {
            get { return "The resource could not be found"; }
        }

        public string Suggestion
        {
            get { return "Check if the resource has been moved or deleted or if the address is incorrect"; }
        }

        public int HttpStatusCode
        {
            get { return 404; }
        }

        public int HttpSubStatusCode
        {
            get { return 0; }
        }

        public System.Collections.Generic.IEnumerable<IResolutionOption> ResolutionOptions
        {
            get { yield break; }
        }

        public object Response
        {
            get { return null; }
        }
    }
}
