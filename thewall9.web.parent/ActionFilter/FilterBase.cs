
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
                        APP._Langs = SiteService.GetLang(APP._SiteID, filterContext.HttpContext.Request.Url.Authority);
                }
            }
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.RouteData.Values.ContainsKey("NoFilterBase"))
            {
                if (!filterContext.IsChildAction && !filterContext.HttpContext.Request.IsAjaxRequest())
                    APP._Site = SiteService.Get(APP._SiteID, filterContext.HttpContext.Request.Url.Authority, APP._CurrentLang);
            }
        }
    }
}