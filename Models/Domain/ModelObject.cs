using System;

using JoshCodes.Web.Models.Persistence;
using JoshCodes.Web.Attributes;

namespace JoshCodes.Web.Models.Domain
{
    public class ModelObject<T>
        where T : IDefineModelObject
    {
        protected T definition;

        protected ModelObject(T definition)
        {
            this.definition = definition;
        }

        public DomainId Id
        {
            get
            {
                return definition.GetDomainId(this.GetType());
            }
        }

        public Uri Urn
        {
            get
            {
                var namespaceAttributes = this.GetType().GetCustomAttributes(typeof(UrnNamespaceIdentifierAttribute), false);
                if (namespaceAttributes == null || namespaceAttributes.Length != 1)
                {
                    throw new Exception(String.Format("Type [{0}] needs JoshCodes.Web.Attributes.UrnNamespaceIdentifierAttribute for URN resolution", this.GetType()));
                }
                var namespaceAttribute = (UrnNamespaceIdentifierAttribute)namespaceAttributes[0];
                var urnNamespace = this.definition.UrnNamespace;
                var urnString = String.Format("urn:{0}:{1}", namespaceAttribute.NsId, String.Join("/", urnNamespace));
                var urn = new Uri(urnString, UriKind.Absolute);
                return urn;
            }
        }

        public virtual DateTimeOffset LastModified
        {
            get
            {
                return definition.LastModified;
            }
        }
    }
}
