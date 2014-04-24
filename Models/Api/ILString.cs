using System;
using System.Collections.Generic;

namespace JoshCodes.Web.Models.Api
{
    public class ILString
    {
        public ILString(string en)
        {
            this.En = en;
        }
        public string En { get; set; }

        public string Es { get; set; }

        public string Fr { get; set; }
    }
}
