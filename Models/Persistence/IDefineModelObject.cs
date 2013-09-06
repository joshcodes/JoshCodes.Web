using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoshCodes.Web.Models.Persistence
{
    public interface IDefineModelObject
    {
        string IdKey { get; }

        Guid IdGuid { get; }

        Uri IdUrn { get; }

        DateTime UpdatedAt { get; }

        DateTime CreatedAt { get; }
    }
}
