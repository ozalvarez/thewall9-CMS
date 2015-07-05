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
        public List<BlogListWeb> Get(int SiteID
            , string Url
            , string Lang
            , int BlogCategoryID
            , int Take
            , int Page)
        {
            return DownloadObject<List<BlogListWeb>>("api/blog?SiteID=" + SiteID 
                + "&Url=" + Url 
                + "&Lang=" + Lang
                + "&BlogCategoryID=" + BlogCategoryID
                + "&Take=" + Take 
                + "&Page=" + Page);
        }
        public BlogPostModelBinding GetDetail(int SiteID, string Url, string FriendlyUrl, int CurrencyID)
        {
            return DownloadObject<BlogPostModelBinding>("api/product?SiteID=" + SiteID + "&Url=" + Url + "&FriendlyUrl=" + FriendlyUrl + "&CurrencyID=" + CurrencyID);
        }
    }
}
