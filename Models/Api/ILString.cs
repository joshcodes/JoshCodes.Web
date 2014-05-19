using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JoshCodes.Web.Models.Api
{
    // TODO: Convert to T4
    [DataContract()]
    public class ILString
    {
        public ILString(string en)
        {
            this.En = en;
        }

        [DataMember()]
        public string En { get; set; }

        [DataMember()]
        public string Es { get; set; }

        [DataMember()]
        public string Fr { get; set; }
    }
}
