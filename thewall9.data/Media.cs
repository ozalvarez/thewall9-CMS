using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data.binding;

namespace thewall9.data
{
    public class Media : MediaBase
    {
        [ForeignKey("SiteID")]
        public virtual Site Site { get; set; }
    }
}
