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
        public BlogListWeb Get(int SiteID
            , string Url
            , string Lang
            , string FriendlyUrl
            , int Page)
        {
            return DownloadObject<BlogListWeb>("api/blog?SiteID=" + SiteID
                + "&Url=" + Url
                + "&Lang=" + Lang
                + "&FriendlyUrl=" + FriendlyUrl
                + "&Page=" + Page);
        }
        public BlogPostWeb GetDetail(int BlogPostID, string FriendlyUrl)
        {
            return DownloadObject<BlogPostWeb>("api/blog?BlogPostID=" + BlogPostID
                + "&FriendlyUrl=" + FriendlyUrl);
        }
    }
}
