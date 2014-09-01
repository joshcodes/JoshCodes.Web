using System;
using System.Collections.Generic;

using JoshCodes.Web.Models.Persistence;

namespace JoshCodes.Web.Models.Domain
{
    public abstract class Store<TObject, TPersistence, TPersistenceStore>
        where TPersistenceStore : IStoreObjects<TPersistence>
    {
        protected TPersistenceStore store;

        protected Store(TPersistenceStore store)
        {
            this.store = store;
        }

        protected abstract TObject ConvertFromPersistence(TPersistence definition);

        public IEnumerable<TObject> All()
        {
            foreach (var persistenceObject in store.All())
            {
                yield return ConvertFromPersistence(persistenceObject);
            }
        }

        public TObject Find(JoshCodes.Web.Models.Domain.DomainId domainId)
        {
            if (domainId == null)
            {
                return default(TObject);
            }
            if (domainId.Guid != Guid.Empty)
            {
                return FindByIdGuid(domainId.Guid);
            }
            if (!String.IsNullOrWhiteSpace(domainId.Key))
            {
                return FindByIdKey(domainId.Key);
            }
            if (domainId.Urn != null)
            {
                return FindByIdUrn(domainId.Urn);
            }
            return default(TObject);
        }

        public TObject FindByIdKey(string idKey)
        {
            TPersistence persistenceObject = store.Find(idKey);
            if (persistenceObject == null)
            {
                return default(TObject);
            }
            return ConvertFromPersistence(persistenceObject);
        }

        public TObject FindByIdGuid(Guid idGuid)
        {
            TPersistence persistenceObject = store.Find(idGuid);
            if (persistenceObject == null)
            {
                return default(TObject);
            }
            return ConvertFromPersistence(persistenceObject);
        }

        public TObject FindByIdUrn(Uri idUrn)
        {
            TPersistence persistenceObject = store.Find(idUrn);
            if (persistenceObject == null)
            {
                return default(TObject);
            }
            return ConvertFromPersistence(persistenceObject);
        }
    }
}