using System;

namespace JoshCodes.Web.Attributes
{
	public class UrnNamespaceIdentifierAttribute : Attribute
	{
		private readonly string nsId;
		
		public UrnNamespaceIdentifierAttribute(string nsId)
		{
			this.nsId = nsId;
		}
		
		public string NsId { get { return nsId; } }
	}
}
