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
        public ProductsWeb Get(int SiteID, string Url,string Lang, string FriendlyUrl, int CurrencyID, int CategoryID, int Take, int Page)
        {
            return DownloadObject<ProductsWeb>("api/product?SiteID=" + SiteID + "&Url=" + Url + "&Lang=" + Lang + "&FriendlyUrl=" + FriendlyUrl + "&CurrencyID=" + CurrencyID + "&CategoryID=" + CategoryID + "&Take=" + Take + "&Page=" + Page);
        }
    }
}
