using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutocadPrinting
{
    class XrefClass
    {
        public string xrefName;
        public string status;

        public XrefClass(string xrefName, string status)
        {
            this.xrefName = xrefName;
            this.status = status;
        }
    }
}
