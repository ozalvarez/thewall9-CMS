using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    #region Base
    //POST
    public class BlogPostBase
    {
        public int BlogPostID { get; set; }
        public int SiteID { get; set; }
        public bool Published { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class BlogPostCultureBase
    {
        public int BlogPostID { get; set; }
        public int CultureID { get; set; }
        public bool Published { get; set; }
        public string Content { get; set; }
        public string FriendlyUrl { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
    }
    //TAG
    public class BlogTagBase
    {
        public int BlogTagID { get; set; }
        public string BlogTagName { get; set; }
    }
    public class BlogPostTagBase
    {
        public int BlogTagID { get; set; }
        public int CultureID { get; set; }
        public int BlogPostID { get; set; }
    }
    //CATEGORY
    public class BlogCategoryBase
    {
        public int BlogCategoryID { get; set; }
    }
    public class BlogCategoryCultureBase
    {
        public int BlogCategoryID { get; set; }
        public int CultureID { get; set; }
        public int BlogCategoryName { get; set; }
    }
    public class BlogPostCategoryBase
    {
        public int BlogPostID { get; set; }
        public int BlogCategoryID { get; set; }
    }
    //FEATURE IMAGE
    //MEDIA
    #endregion

    #region Binding
    public class BlogPostBinding : BlogPostBase
    {
    }
    #endregion
}
