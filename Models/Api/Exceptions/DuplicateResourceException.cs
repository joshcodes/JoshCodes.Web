using System;
using System.Collections.Generic;

namespace JoshCodes.Web.Models.Api
{
    public class DuplicateResourceException : Exception, IAPIException
    {
        private WebId id;

        public DuplicateResourceException(WebId id, DateTime lastModified)
        {
            this.id = id;
        }

        public string Reason
        {
            get
            {
                return String.Format("The server already has a resource with the ID [{0}]", this.id.Guid);
            }
        }

        public string Suggestion
        {
            get
            {
                return String.Format(
                    "Consider updating the existing resource at [{0}] if the Last-Modified time of the new resource is greater",
                    this.id.Source);
            }
        }

        public int HttpStatusCode
        {
            get { return 409; }
        }

        public int HttpSubStatusCode
        {
            get { return 1044; }
        }

        public IEnumerable<IResolutionOption> ResolutionOptions
        {
            get
            {
                yield return new ResolutionOption(
                    "Update Exisiting",
                    "Update the existing object",
                    ResolutionOptionActions.Put,
                    id.Source,
                    1044);
            }
        }

        public object Response
        {
            get { return id; }
        }
    }
}
