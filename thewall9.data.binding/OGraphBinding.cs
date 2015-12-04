using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    public class OGraphBase
    {
        public int OGraphID { get; set; }
        public string OGraphTitle { get; set; }
        public string OGraphDescription { get; set; }
    }
    public class OGraphMediaBase
    {
        public int OGraphID { get; set; }
        public int MediaID { get; set; }
    }

    public class OGraphBinding : OGraphBase
    {
        public FileRead FileRead { get; set; }
    }
    
}
