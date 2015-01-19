using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using thewall9.data.binding;

namespace thewall9.web.parent.BLL
{
    public class SiteBLL : BaseBLL
    {
        public SiteFullBinding Get(int SiteID,string Url, string Lang)
        {
            return DownloadObject<SiteFullBinding>("api/site?SiteID=" + SiteID + "&Url=" + Url +"&Lang=" + Lang);
        }
    }
}