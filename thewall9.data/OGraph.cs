using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data.binding;

namespace thewall9.data
{
    public class OGraph : OGraphBase
    {
        public virtual OGraphMedia OGraphMedia { get; set; }
    }
    public class OGraphMedia : OGraphMediaBase
    {
        [ForeignKey("MediaID")]
        public virtual Media Media { get; set; }
        [ForeignKey("OGraphID")]
        public virtual OGraph OGraph { get; set; }
    }
}
