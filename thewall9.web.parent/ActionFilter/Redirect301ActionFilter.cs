using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;

namespace thewall9.web.parent.ActionFilter
{
    public class Redirect301ActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.IsChildAction)
            {
                HttpRequestBase _Request = filterContext.HttpContext.Request;
                string _Url = _Request.Url.ToString();
                if (!_Request.IsLocal)
                {
                    if (_Url.Contains("://www."))
                    {
                        if (!_Request.IsSecureConnection)
                            _Url = _Url.Replace("http://www.", "http://");
                        else
                            _Url = _Url.Replace("https://www.", "http://");
                        filterContext.Result = new RedirectResult(_Url, true);
                        filterContext.Result.ExecuteResult(filterContext);
                        base.OnActionExecuting(filterContext);
                    }
                }
            }
        }
    }
}
