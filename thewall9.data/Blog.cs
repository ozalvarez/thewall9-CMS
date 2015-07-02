using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data.binding;
using thewall9.data.Models;

namespace thewall9.data
{
    public class BlogPost : BlogPostBase
    {
        [ForeignKey("SiteID")]
        public virtual Site Site { get; set; }
        [ForeignKey("UserIDCreator")]
        public virtual ApplicationUser User { get; set; }
    }
    public class BlogPostCulture : BlogPostCultureBase
    {
        [ForeignKey("BlogPostID")]
        public virtual BlogPost BlogPost { get; set; }
        [ForeignKey("CultureID")]
        public virtual Culture Culture { get; set; }
        
    }
}
