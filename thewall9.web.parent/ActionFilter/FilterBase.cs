
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using thewall9.web.parent.BLL;
using thewall9.web.parent.HtmlHelpers;
namespace thewall9.web.parent.ActionFilter
{
    public class FilterBase : ActionFilterAttribute
    {
        SiteBLL SiteService = new SiteBLL();
        PageBLL PageService = new PageBLL();
        ContentBLL ContentService = new ContentBLL();
        List<string> _Bots = new List<string>() { "googlebot", "bingbot" };

        private string GetCulture(HttpRequestBase Request, string DefaultCulture)
        {
            string CultureName = null;
            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_Culture"];
            if (cultureCookie != null)
                CultureName = cultureCookie.Value;
            else
            {
                CultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;
            }
            CultureName = CultureHelper.GetImplementedCulture(CultureName, DefaultCulture); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(CultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            return CultureName;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.RouteData.Values.ContainsKey("NoFilterBase"))
            {
                if (!filterContext.IsChildAction && !filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    /*
                     * MICROSOFT BUG
                     * http://stackoverflow.com/questions/20737578/asp-net-sessionid-owin-cookies-do-not-send-to-browser/21234614#21234614
                     * https://katanaproject.codeplex.com/workitem/197
                     */
                    filterContext.HttpContext.Session["Workaround"] = 0;
                    if (APP._Langs == null || filterContext.HttpContext.Request.IsLocal)
                    {
                        APP._Langs = SiteService.GetLang(APP._SiteID, filterContext.HttpContext.Request.Url.Authority);
                        APP._CurrentLang = APP._Langs[0].Name;
                        APP._CurrentFriendlyUrl = APP._Langs[0].FriendlyUrl;
                    }
                    var _CultureName = GetCulture(filterContext.HttpContext.Request, APP._CurrentLang);
                    var _SavedLang = APP._Langs.Where(m => _CultureName.Contains(m.Name)).FirstOrDefault();
                    if (_SavedLang != null)
                    {
                        APP._CurrentLang = _SavedLang.Name;
                        APP._CurrentFriendlyUrl = _SavedLang.FriendlyUrl;
                    }
                }
            }
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.RouteData.Values.ContainsKey("NoFilterBase"))
            {
                if (!filterContext.IsChildAction && !filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    APP._Site = SiteService.Get(APP._SiteID, filterContext.HttpContext.Request.Url.Authority, APP._CurrentLang, APP._CurrentCurrencyID);
                    if (APP._Site.Site.ECommerce)
                    {
                        if (APP._CurrentCurrencyID == 0)
                            APP._CurrentCurrencyID = APP._Site.Currencies[0].CurrencyID;
                    }
                }
            }
        }
    }
}