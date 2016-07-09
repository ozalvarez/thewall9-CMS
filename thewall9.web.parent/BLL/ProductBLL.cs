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
        public ProductsWeb Get(string Url
            , string ProductCategoryFriendlyUrl
            , int Page)
        {
            return DownloadObject<ProductsWeb>("api/product?SiteID=" + APP._SiteID
                + "&Url=" + Url
                + "&Lang=" + APP._CurrentLang
                + "&CurrencyID=" + APP._CurrentCurrencyID
                + "&ProductCategoryFriendlyUrl=" + ProductCategoryFriendlyUrl
                + "&Page=" + Page);
        }
        public ProductWeb GetDetail(string Url
            , int ProductID
            , string FriendlyUrl)
        {
            return DownloadObject<ProductWeb>("api/product?SiteID=" + APP._SiteID
                + "&Url=" + Url
                + "&ProductID=" + ProductID
                + "&FriendlyUrl=" + FriendlyUrl
                + "&CurrencyID=" + APP._CurrentCurrencyID);
        }
    }
}
