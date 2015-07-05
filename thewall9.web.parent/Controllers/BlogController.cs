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
        [Route("blog")]
        public ActionResult Index(int? BlogCategoryID, int? Page)
        {
            return View(_BlogService.Get(APP._SiteID
                , Request.Url.Authority
                , APP._CurrentLang, 0, 10, 1));
        }
    }
}