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
        public static string _CurrentLang
        {
            get
            {
                return HttpContext.Current.Items["CurrentLang"] as string;
            }
            set
            {
                HttpContext.Current.Items["CurrentLang"] = value;
            }
        }
        public static string _CurrentFriendlyUrl
        {
            get
            {
                return HttpContext.Current.Items["CurrentFriendlyUrl"] as string;
            }
            set
            {
                HttpContext.Current.Items["CurrentFriendlyUrl"] = value;
            }
        }
        public static List<CultureRoutes> _Langs
        {
            get
            {
                return HttpContext.Current.Session["Langs"] as List<CultureRoutes>;
            }
            set
            {
                HttpContext.Current.Session["Langs"] = value;
            }
        }
        public static int _CurrentCurrencyID
        {
            get
            {
                try
                {
                    return Convert.ToInt32(HttpContext.Current.Request.Cookies["_CurrentCurrencyID"]);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Request.Cookies.Add(new HttpCookie(value.ToString()));
            }
        }
    }
}