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
    //POST
    public class BlogPost : BlogPostBase
    {
        [ForeignKey("SiteID")]
        public virtual Site Site { get; set; }
    }
    public class BlogPostCulture : BlogPostCultureBase
    {
        [ForeignKey("BlogPostID")]
        public virtual BlogPost BlogPost { get; set; }
        [ForeignKey("CultureID")]
        public virtual Culture Culture { get; set; }
    }
    //TAG
    public class BlogTag : BlogTagBase
    {

    }
    public class BlogPostTag : BlogPostTagBase
    {
        [ForeignKey("BlogPostID,CultureID")]
        public virtual BlogPostCulture BlogPostCulture { get; set; }
        [ForeignKey("BlogTagID")]
        public virtual BlogTag BlogTag { get; set; }
    }

    //CATEGORY
    public class BlogCategory : BlogCategoryBase
    {

    }
    public class BlogCategoryCulture : BlogCategoryCultureBase
    {
        [ForeignKey("BlogCategoryID")]
        public BlogCategory BlogCategory { get; set; }
        [ForeignKey("CultureID")]
        public virtual Culture Culture { get; set; }
    }
    public class BlogPostCategory : BlogPostCategoryBase
    {
        [ForeignKey("BlogPostID")]
        public virtual BlogPost BlogPost { get; set; }
        [ForeignKey("BlogCategoryID")]
        public BlogCategory BlogCategory { get; set; }
    }
}
