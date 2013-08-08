using System;
using JoshCodes.Core.Urns.Extensions;

namespace JoshCodes.Web.Uris.Extensions
{
	public static class UriExtensions
	{
		public static Guid ParseWebUri (this Uri uri, out string nid, out string ns)
		{
			if (String.Equals (uri.Scheme, "urn", StringComparison.OrdinalIgnoreCase))
			{
				return uri.ParseWebUrn(out nid, out ns);
			}
			var parameters = System.Web.HttpUtility.ParseQueryString (uri.Query);
			var urnString = parameters.Get("self");
			var urn = new Uri(urnString);
			return urn.ParseWebUrn(out nid, out ns);
		}

		public static Guid ParseWebUrn(this Uri urn, out string nid, out string ns)
		{
			string [] compositeNs = urn.ParseUrnNamespaceString(out nid);
			if(compositeNs.Length != 2)
			{
				throw new ArgumentException(String.Format("URN[{0}] is not a Web URN", urn), "urn");
			}
			Guid guid;
			if(!Guid.TryParse(compositeNs[1], out guid))
			{
				throw new ArgumentException(String.Format("Invalid UUID[{0}] in URN[{1}]", compositeNs[1], urn), "urn");
			}
			ns = compositeNs[0];
			return guid;
		}

		public static string ParseWebUrnString(this Uri urn, out string nid, out string ns)
		{
			string [] compositeNs = urn.ParseUrnNamespaceString(out nid);
			if(compositeNs.Length != 2)
			{
				throw new ArgumentException(String.Format("URN[{0}] is not a Web URN", urn), "urn");
			}
			ns = compositeNs[0];
			string id = compositeNs[1];
			return id;
		}

		public static Uri ToWebUrn (this Guid guid, string nid, string ns)
		{
			string uriStr = String.Format("urn:{0}:{1}:{2}", nid, ns, guid.ToString());
			Uri uri;
			if(!Uri.TryCreate(uriStr, UriKind.Absolute, out uri))
			{
				throw new ArgumentException("Invalid id or namespace for creating a web URN");
			}
			return uri;
		}

		public static Uri ToWebUrn (this string id, string nid, string ns)
		{
			string uriStr = String.Format("urn:{0}:{1}:{2}", nid, ns, id);
			Uri uri;
			if(!Uri.TryCreate(uriStr, UriKind.Absolute, out uri))
			{
				throw new ArgumentException("Invalid id or namespace for creating a web URN");
			}
			return uri;
		}
	}
}

