using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    public class CurrencyBase
    {
        public int CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public bool Default { get; set; }
        public int SiteID { get; set; }
    }
    public class CurrencyBinding:CurrencyBase
    {
    }
}
