using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JoshCodes.Web.Attributes
{
    [AttributeUsageAttribute(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AcceptVerbsByParameterAttribute : Attribute
    {
        private static List<System.Web.Mvc.HttpVerbs> AcceptedVerbsForActionUri = new List<HttpVerbs>() { HttpVerbs.Delete, HttpVerbs.Get, HttpVerbs.Head };

        public string ParameterName { get; set; }
        public List<System.Web.Mvc.HttpVerbs> Verbs { get; set; }

        public AcceptVerbsByParameterAttribute(string parameterName, params System.Web.Mvc.HttpVerbs[] verbs)
        {
            this.ParameterName = parameterName;
            this.Verbs = verbs.Distinct().ToList();
        }

        public AcceptVerbsByParameterAttribute(string parameterName, params string[] verbs)
        {
            this.ParameterName = parameterName;

            this.Verbs = new List<System.Web.Mvc.HttpVerbs>();
            foreach(var verb in verbs)
            {
                System.Web.Mvc.HttpVerbs httpVerb;
                if(this.GetHttpVerb(verb, out httpVerb) && !this.Verbs.Contains(httpVerb))
                {
                    this.Verbs.Add(httpVerb);
                }
                else
                {
                    throw new System.ArgumentException(string.Format(@"Http verb ""{0}"" not recognized in AcceptVerbsByParameterAttribute.", verb));
                }
            }
        }

        #region Public Methods

        public bool ShouldUseParameterInActionUri()
        {
            return AcceptedVerbsForActionUri.Intersect(this.Verbs).Count() > 0;
        }

        #endregion

        #region Helper Methods

        private bool GetHttpVerb(string verb, out System.Web.Mvc.HttpVerbs httpVerb)
        {
            return HttpVerbs.TryParse(verb, out httpVerb);
        }

        #endregion
    }

    public static class AcceptVerbsByParameterAttributeExtensions
    {
        public static AcceptVerbsByParameterAttribute GetAcceptVerbsByParameterAttributeForParameter(this AcceptVerbsByParameterAttribute[] acceptVerbsByParameterAttributes, string parameterName)
        {
            AcceptVerbsByParameterAttribute parameterAttrbiute = null;

            foreach(var acceptVerbsByParameterAttribute in acceptVerbsByParameterAttributes)
            {
                if(acceptVerbsByParameterAttribute.ParameterName == parameterName)
                {
                    parameterAttrbiute = acceptVerbsByParameterAttribute;
                    break;
                }
            }

            return parameterAttrbiute;
        }
    }
}