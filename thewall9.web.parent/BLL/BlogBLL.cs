using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using thewall9.data.binding;
using Newtonsoft.Json;
using Elmah;

namespace thewall9.web.parent.BLL
{
    public class BlogBLL : BaseBLL
    {
        public BlogListWeb Get(string Url
            , string Lang
            , string BlogCategoryFriendlyUrl
            , string BlogTagName
            , int Page)
        {
            return DownloadObject<BlogListWeb>("api/blog?SiteID=" + APP._SiteID
                + "&Url=" + Url
                + "&Lang=" + Lang
                + "&BlogCategoryFriendlyUrl=" + BlogCategoryFriendlyUrl
                + "&BlogTagName=" + BlogTagName
                + "&Page=" + Page);
        }
        public BlogPostWeb GetDetail(string Url, int BlogPostID, string FriendlyUrl)
        {
            return DownloadObject<BlogPostWeb>("api/blog?SiteID=" + APP._SiteID
                + "&Url=" + Url
                + "&BlogPostID=" + BlogPostID
                + "&FriendlyUrl=" + FriendlyUrl);
        }
    }
}
