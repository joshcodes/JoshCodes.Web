using System;
using JoshCodes.Web.Attributes;

namespace JoshCodes.Web.Models.Persistence
{
	[UrnNamespaceIdentifier("x-generic-filestore")]
	public interface IStoreFiles : IStoreObjects<IDefineFile>
	{
    }
}
