using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    public class CultureBase
    {
        public int CultureID { get; set; }
        public int SiteID { get; set; }
        public string Name { get; set; }
    }
    public class CultureBinding: CultureBase
    {
        
    }
    public class CultureImport
    {
        public int CultureIDOld { get; set; }
        public int CultureIDNew { get; set; }
    }
    public class CultureRoutes : CultureBase
    {
        public string FriendlyUrl { get; set; }
    }
}
