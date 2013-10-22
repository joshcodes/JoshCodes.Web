using System;
using System.Runtime.Serialization;

namespace JoshCodes.Web.Models.Api
{
    [DataContract(Name="web_id")]
    public class WebId
    {
        public WebId()
        {
        }

        public WebId(string key, Guid guid, Uri urn, Uri source)
        {
            this.Key = key;
            this.Guid = guid;
            this.Urn = urn;
            this.Source = source;
        }

        /// <summary>
        /// This is a string that uniquely identifies this object among similar objects. While
        /// it is not guarenteed to be globally unique, it is often a GUID.
        /// </summary>
        [DataMember(Name = "key")]
        public string Key { get; set; }

        /// <summary>
        /// This is a globally unique identifier of this object. However, this does not contain
        /// information about what type of object this is or where it is located.
        /// </summary>
        [DataMember(Name = "guid")]
        public Guid Guid { get; set; }
        
        /// <summary>
        /// The URN identifier is globally unique and includes information about what type of
        /// object is being identified but does not include information about where it is located.
        /// </summary>
        [DataMember(Name = "urn")]
        public Uri Urn { get; set; }

        /// <summary>
        /// The source identifier specifies the authoritative location where the object
        /// can be accessed / updated.
        /// </summary>
        [DataMember(Name = "source")]
        public Uri Source { get; set; }

        public bool IsEmpty()
        {
            return
                String.IsNullOrWhiteSpace(Key) &&
                (this.Guid == default(Guid) || this.Guid == Guid.Empty) &&
                Urn == null &&
                Source == null;
        }
    }
}
