using System;

using JoshCodes.Web.Models.Persistence;
using JoshCodes.Web.Attributes.Extensions;

namespace JoshCodes.Web.Models.Domain
{
    public static class Extensions
    {
        public static DomainId ToDomainId(this Api.WebId webId)
        {
            if (webId == null)
            {
                return null;
            }
            return new DomainId(webId.Key, webId.Guid, webId.Urn);
        }

        public static bool IsSame<T>(this ModelObject<T> modelObject1, ModelObject<T> modelObject2)
            where T : IDefineModelObject
        {
            var modelId1 = modelObject1.Id;
            var modelId2 = modelObject2.Id;
            return (modelId1.Guid != default(Guid) && modelId1.Guid != Guid.Empty && modelId1.Guid == modelId2.Guid);
        }

        internal static DomainId GetDomainId<TModelDefinition>(this TModelDefinition modelObjectDefinition, Type domainObjectType)
            where TModelDefinition : IDefineModelObject
        {
            if (domainObjectType.BaseType.GenericTypeArguments[0].GUID != typeof(TModelDefinition).GUID)
            {
                string message = String.Format("The type for the requested domain id [{0}] does not match the definition [{1}]",
                    domainObjectType.FullName, typeof(TModelDefinition).FullName);
                throw new ArgumentException(message, "domainObjectType");
            }

            var key = modelObjectDefinition.Key;
            var namespaceId = domainObjectType.GetUrnNamespaceIdentifier(false);
            if (String.IsNullOrWhiteSpace(namespaceId))
            {
                throw new ArgumentException(
                    String.Format("Model object of type {0} does not have UrnNamespaceAttribute", domainObjectType.FullName),
                    "this");
            }
            var urnNamespace = modelObjectDefinition.UrnNamespace;
            var urnString = String.Format("urn:{0}:{1}", namespaceId, String.Join("/", urnNamespace));
            var urn = new Uri(urnString, UriKind.Absolute);
            return new DomainId(key.ToString(), key, urn);
        }

        public static DomainId GetDomainId<TDomainObject, TModelDefinition>(this TModelDefinition modelObjectDefinition)
            where TModelDefinition : IDefineModelObject
            where TDomainObject : ModelObject<TModelDefinition>
        {
            return modelObjectDefinition.GetDomainId(typeof(TDomainObject));
        }
    }
}
