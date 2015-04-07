using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    public class TagBase
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public int SiteID { get; set; }
    }
    public class TagBinding : TagBase
    {
    }
}
