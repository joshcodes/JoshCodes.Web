using System;
using JoshCodes.Core.Extensions;

namespace JoshCodes.Web.Attributes.Extensions
{
	public static class GetAttributeExtensions
	{
		public static UrnNamespaceIdentifierAttribute GetUrnNamespaceIdentifierAttribute(this Type type, bool inherit)
		{
            var niAttr = type.GetCustomAttribute<UrnNamespaceIdentifierAttribute>(inherit);
            if (niAttr == null && inherit)
            {
                foreach (var typeInterface in type.GetInterfaces())
                {
                    niAttr = typeInterface.GetUrnNamespaceIdentifierAttribute(true);
                    if(niAttr != null)
                    {
                        break;
                    }
                }
            }
            return niAttr;
		}

		public static string GetUrnNamespaceIdentifier(this Type type, bool inherit)
		{
            var niAttr = type.GetUrnNamespaceIdentifierAttribute(inherit);
			return niAttr == null ? String.Empty : niAttr.NsId;
		}
	}
}

