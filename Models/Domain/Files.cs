using JoshCodes.Web.Models.Persistence;

namespace JoshCodes.Web.Models.Domain
{
	public class Files : Store<File, IDefineFile, IStoreFiles>
	{
        public Files(IStoreFiles store)
            : base(store)
		{
		}

        protected override File ConvertFromPersistence(IDefineFile definition)
        {
            return new File(definition);
        }
    }
}
