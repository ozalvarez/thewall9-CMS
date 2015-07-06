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
    public class ProductBLL : BaseBLL
    {
        public ProductsWeb Get(int SiteID
            , string Url
            , string ProductCategoryFriendlyUrl
            , int Page)
        {
            return DownloadObject<ProductsWeb>("api/product?SiteID=" + SiteID
                + "&Url=" + Url
                + "&Lang=" + APP._CurrentLang
                + "&CurrencyID=" + APP._CurrentCurrencyID
                + "&ProductCategoryFriendlyUrl=" + ProductCategoryFriendlyUrl
                + "&Page=" + Page);
        }
        public ProductWeb GetDetail(int SiteID
            , string Url
            , string FriendlyUrl
            , int CurrencyID)
        {
            return DownloadObject<ProductWeb>("api/product?SiteID=" + SiteID + "&Url=" + Url + "&FriendlyUrl=" + FriendlyUrl + "&CurrencyID=" + CurrencyID);
        }
    }
}
