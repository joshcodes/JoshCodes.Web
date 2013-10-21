using System;

using JoshCodes.Web.Models.Persistence;

namespace JoshCodes.Web.Models.Domain
{
    public class ModelObject<T>
        where T : IDefineModelObject
    {
        protected T definition;

        protected ModelObject(T definition)
        {
            this.definition = definition;
            this.Id = new DomainId(definition.IdKey, definition.IdGuid, definition.IdUrn);
        }

        public DomainId Id { get; private set; }

        public DateTime LastModified
        {
            get
            {
                return definition.UpdatedAt;
            }
        }
    }
}
