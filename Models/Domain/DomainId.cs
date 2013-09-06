using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoshCodes.Web.Models.Domain
{
    public class DomainId
    {
        public DomainId()
        {
        }

        public DomainId(string key, Guid guid, Uri urn)
        {
            this.Key = key;
            this.Guid = guid;
            this.Urn = urn;
        }

        /// <summary>
        /// This is a string that uniquely identifies this object among similar objects. While
        /// it is not guarenteed to be globally unique, it is often a GUID.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// This is a globally unique identifier of this object. However, this does not contain
        /// information about what type of object this is or where it is located.
        /// </summary>
        public Guid Guid { get; set; }
        
        /// <summary>
        /// The URN identifier is globally unique and includes information about what type of
        /// object is being identified but does not include information about where it is located.
        /// </summary>
        public Uri Urn { get; set; }
    }
}
