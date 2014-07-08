using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NC2.CPM.Metadata
{
    public class Phone
    {
        string phone;



        public String AreaCode
        {
            get
            {
                // TODO: Look for -'s, etc -- generally be more smart about this
                return phone.Substring(0, 3);
            }
        }
    }
}
