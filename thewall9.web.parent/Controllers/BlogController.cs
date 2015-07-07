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
    public class BlogController : Controller
    {
        BlogBLL _BlogService = new BlogBLL();
        ContentBLL _ContentService = new ContentBLL();

        [Route("blog/{BlogCategoryFriendlyUrl?}")]
        public ActionResult Index(string BlogCategoryFriendlyUrl, int Page = 1)
        {
            ViewBag.Page = Page;
            ViewBag.BlogCategoryFriendlyUrl = BlogCategoryFriendlyUrl;

            ViewBag.BlogContent = _ContentService.Get(Request.Url.Authority, "blog");

            return View(_BlogService.Get(APP._SiteID
                , Request.Url.Authority
                , APP._CurrentLang, BlogCategoryFriendlyUrl, null, Page));
        }
        [Route("blog/tag/{BlogTagName}")]
        public ActionResult Tag(string BlogTagName, int Page = 1)
        {
            ViewBag.Page = Page;
            ViewBag.BlogTagName = BlogTagName;

            ViewBag.BlogContent = _ContentService.Get(Request.Url.Authority, "blog");

            return View("Index",_BlogService.Get(APP._SiteID
                , Request.Url.Authority
                , APP._CurrentLang, null, BlogTagName, Page));
        }
        [Route("post/{BlogPostID}/{FriendlyUrl}")]
        public ActionResult Detail(int BlogPostID, string FriendlyUrl)
        {
            return View(_BlogService.GetDetail(BlogPostID, FriendlyUrl));
        }
    }
}