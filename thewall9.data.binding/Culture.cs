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
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string GPlus { get; set; }
        public string Tumblr { get; set; }
        public string Instagram { get; set; }
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
