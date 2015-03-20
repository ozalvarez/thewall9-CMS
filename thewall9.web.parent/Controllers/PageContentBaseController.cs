using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using thewall9.web.parent.BLL;
using thewall9.web.parent.HtmlHelpers;

namespace thewall9.web.parent.Controllers
{
    public class PageContentBaseController : Controller
    {
        PageBLL PageService = new PageBLL();

        private string GetCulture()
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
                // Validate culture name
                // cultureName = "es";
            }
            CultureName = CultureHelper.GetImplementedCulture(CultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(CultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            return CultureName;
        }

        public ActionResult Index(string FriendlyUrl)
        {
            if (string.IsNullOrEmpty(FriendlyUrl))
            {
                var _CultureName = GetCulture();
                var _DatabaseCulture=APP._Langs.Where(m => _CultureName.Contains(m.Name)).FirstOrDefault();
                if (_DatabaseCulture != null && !string.IsNullOrEmpty(_DatabaseCulture.FriendlyUrl))
                {
                    return Redirect("/" + _DatabaseCulture.FriendlyUrl);
                }
            }
            var _Model = PageService.Get(FriendlyUrl, APP._SiteID, Request.Url.Authority);
            if (_Model == null)
                throw new HttpException(404, "Page Not Found");
            else
            {
                if (!string.IsNullOrEmpty(_Model.Page.RedirectUrl))
                    return Redirect(_Model.Page.RedirectUrl);
                ViewBag.Title = _Model.Page.TitlePage;
                ViewBag.MetaDescription = _Model.Page.MetaDescription;
                APP._CurrentLang = _Model.Page.CultureName;
                return View(_Model.Page.ViewRender, _Model);
            }
        }
        public ActionResult Error()
        {
            return View();
        }
        [Route("sitemap.xml")]
        [OutputCache(Duration = 12 * 3600, VaryByParam = "*")]
        public ActionResult SiteMap()
        {
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            var items = PageService.GetSitemap(APP._SiteID, Request.Url.Authority);
            var sitemap = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement(ns + "urlset",
                from i in items
                select
                new XElement(ns + "url",
                    new XElement(ns + "loc", Request.Url.Scheme + "://" + Request.Url.Authority + "/" + i.FriendlyUrl),
                    new XElement(ns + "lastmod", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                    new XElement(ns + "changefreq", "always"),
                    new XElement(ns + "priority", "0.5")
            )));
            return Content("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + sitemap.ToString(), "text/xml");
        }
        [Route("change-lang")]
        public ActionResult ChangeLang(string Lang, string FriendlyUrl)
        {
            HttpCookie cookie = Request.Cookies["_Culture"];
            if (cookie != null)
                cookie.Value = Lang;
            else
            {
                cookie = new HttpCookie("_Culture");
                cookie.Value = Lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            var _UrlRedirect = "/" + PageService.GetPageFriendlyUrl(APP._SiteID, Request.Url.Authority, FriendlyUrl, Lang);
            return Redirect(_UrlRedirect);
        }
    }
}