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
    [Route("content")]
    public class ContentController : Controller
    {
        ContentBLL _ContentService = new ContentBLL();

        public PartialViewResult Index(string Alias, string View)
        {
            return PartialView(View,_ContentService.Get(Request.Url.Authority, Alias));
        }
    }
}