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

            ViewBag.Title = HtmlModel.FindValue(ViewBag.BlogContent, "blog-title", true).ToString();
            if (!string.IsNullOrEmpty(BlogCategoryFriendlyUrl))
                ViewBag.Title += " | " + BlogCategoryFriendlyUrl;

            ViewBag.MetaDescription = HtmlModel.FindValue(ViewBag.BlogContent, "blog-subtitle", true).ToString();

            return View(_BlogService.Get(Request.Url.Authority
                , APP._CurrentLang, BlogCategoryFriendlyUrl, null, Page));
        }
        [Route("blog/tag/{BlogTagName}")]
        public ActionResult Tag(string BlogTagName, int Page = 1)
        {
            ViewBag.Page = Page;
            ViewBag.BlogTagName = BlogTagName;

            ViewBag.BlogContent = _ContentService.Get(Request.Url.Authority, "blog");

            ViewBag.Title = HtmlModel.FindValue(ViewBag.BlogContent, "blog-title", true).ToString() + " | " + BlogTagName;
            ViewBag.MetaDescription = HtmlModel.FindValue(ViewBag.BlogContent, "blog-subtitle", true).ToString() + " | " + BlogTagName;

            return View("Index", _BlogService.Get(Request.Url.Authority
                , APP._CurrentLang, null, BlogTagName, Page));
        }
        [Route("post/{BlogPostID}/{FriendlyUrl}")]
        public ActionResult Detail(int BlogPostID, string FriendlyUrl)
        {
            ViewBag.BlogContent = _ContentService.Get(Request.Url.Authority, "blog");

            var _Model = _BlogService.GetDetail(Request.Url.Authority, BlogPostID, FriendlyUrl);

            ViewBag.Title = _Model.Title;
            ViewBag.MetaDescription = _Model.ContentPreview;

            return View(_Model);
        }
    }
}