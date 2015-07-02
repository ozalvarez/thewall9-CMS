using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    #region Base
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
    public class BlogTags
    {

    }
    #endregion

    #region Binding
    public class BlogPostBinding : BlogPostBase
    {
    }
    #endregion
}
