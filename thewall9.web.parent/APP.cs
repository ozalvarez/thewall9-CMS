using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using thewall9.data.binding;
namespace thewall9.web.parent
{
    public static class APP
    {

        public static SiteFullBinding _Site
        {
            get
            {
                return HttpContext.Current.Session["Site"] as SiteFullBinding;
            }
            set
            {
                HttpContext.Current.Session["Site"] = value;
            }
        }
        public static int _SiteID
        {
            get
            {
                return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SiteID"]);
            }
        }
        public static string _API_URL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["API_URL"];
            }
        }
        public static string _Lang
        {
            get
            {
                return HttpContext.Current.Items["_Lang"] as string;
            }
            set
            {
                HttpContext.Current.Items["_Lang"] = value;
            }
        }

    }
}