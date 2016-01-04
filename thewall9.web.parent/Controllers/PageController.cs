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
    public class PageController : Controller
    {
        PageBLL PageService = new PageBLL();
        ProductBLL ProductService = new ProductBLL();
        private const int PAGE_SIZE = 10;

        public ActionResult Index(string FriendlyUrl)
        {
            if (string.IsNullOrEmpty(FriendlyUrl))
            {
                if (!string.IsNullOrEmpty(APP._CurrentFriendlyUrl))
                    return Redirect("/" + APP._CurrentFriendlyUrl);
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
                ViewBag.Active = "page-" + FriendlyUrl;

                ViewBag.OGraph = _Model.Page.OGraph;

                APP._CurrentLang = _Model.Page.CultureName;
                return View(_Model.Page.ViewRender, _Model);
            }
        }
        public ActionResult Index2(string FriendlyUrl1, string FriendlyUrl2)
        {
            return Index(FriendlyUrl1 + "/" + FriendlyUrl2);
        }

        public ActionResult Error()
        {
            return View();
        }
        [Route("sitemap.xml")]
        [OutputCache(Duration = 12 * 3600, VaryByParam = "*")]
        public ActionResult SiteMap()
        {
            var _Pages = PageService.GetSitemap(APP._SiteID, Request.Url.Authority);
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            var _Q = from i in _Pages.Pages
                     select
                     new XElement(ns + "url",
                         new XElement(ns + "loc", Request.Url.Scheme + "://" + Request.Url.Authority + "/" + i.FriendlyUrl)
                         , new XElement(ns + "changefreq", "always"));

            if (_Pages.Blog)
            {
                _Q = _Q.Union((from i in _Pages.Posts
                               select
                               new XElement(ns + "url",
                                   new XElement(ns + "loc", Url.Action("detail", "blog", new { BlogPostID = i.BlogPostID, FriendlyUrl = i.FriendlyUrl }, Request.Url.Scheme))
                                   , new XElement(ns + "changefreq", "always"))))
                                   .Union((from i in _Pages.BlogCategories
                                           select
                                           new XElement(ns + "url",
                                               new XElement(ns + "loc", Url.Action("index", "blog", new { BlogCategoryFriendlyUrl = i.FriendlyUrl }, Request.Url.Scheme))
                                               , new XElement(ns + "changefreq", "always"))))
                                               .Union((from i in _Pages.BlogTags
                                                       select
                                                       new XElement(ns + "url",
                                                           new XElement(ns + "loc", Url.Action("tag", "blog", new { BlogTagName = i.BlogTagName }, Request.Url.Scheme))
                                                           , new XElement(ns + "changefreq", "always"))));
            }
            var sitemap = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement(ns + "urlset", _Q));
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
        [Route("change-currency")]
        public ActionResult ChangeCurrency(int CurrencyID, string Url)
        {
            APP._CurrentCurrencyID = CurrencyID;
            return Redirect(Url);
        }

    }
}