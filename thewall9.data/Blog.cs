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
        public BlogPost()
        {
            this.DateCreated = DateTime.Now;
        }
        public BlogPost(int SiteID):this()
        {
            this.SiteID = SiteID;
        }

        [ForeignKey("SiteID")]
        public virtual Site Site { get; set; }
        public virtual List<BlogPostCulture> BlogPostCultures { get; set; }
        public virtual List<BlogPostCategory> BlogPostCategories { get; set; }
    }
    public class BlogPostCulture : BlogPostCultureBase
    {
         public BlogPostCulture()
        {
            this.DateCreated = DateTime.Now;
        }
         public BlogPostCulture(BlogPostModelBinding Model)
             : this()
        {
            this.CultureID = Model.CultureID;
            this.FriendlyUrl =Model.FriendlyUrl;
            UpdateContent(Model);
        }
         public void UpdateContent(BlogPostModelBinding Model)
         {
             this.Title = Model.Title;
             this.Published = Model.Published;
             this.Content = Model.Content;
         }

        [ForeignKey("BlogPostID")]
        public virtual BlogPost BlogPost { get; set; }
        [ForeignKey("CultureID")]
        public virtual Culture Culture { get; set; }
        public virtual List<BlogPostTag> BlogPostTags { get; set; }
    }
    //TAG
    public class BlogTag : BlogTagBase
    {
        public BlogTag(){}
        public BlogTag(string BlogTagName)
            : this()
        {
            this.BlogTagName = BlogTagName;
        }
    }
    public class BlogPostTag : BlogPostTagBase
    {
        public BlogPostTag(){}
        public BlogPostTag(BlogTagModelBinding Model, int BlogTagID, int CultureID)
            : this()
        {
            this.BlogTagID = BlogTagID;
            this.BlogPostID = Model.BlogPostID;
            this.CultureID = CultureID;
        }
        [ForeignKey("BlogPostID,CultureID")]
        public virtual BlogPostCulture BlogPostCulture { get; set; }
        [ForeignKey("BlogTagID")]
        public virtual BlogTag BlogTag { get; set; }

    }

    //CATEGORY
    public class BlogCategory : BlogCategoryBase
    {
        [ForeignKey("SiteID")]
        public virtual Site Site { get; set; }
        public virtual List<BlogCategoryCulture> BlogCategoryCultures { get; set; }
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
