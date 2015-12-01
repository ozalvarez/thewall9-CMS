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
    [Route("product")]
    public class ProductController : Controller
    {
        ProductBLL _ProductService = new ProductBLL();
        //  ContentBLL _ContentService = new ContentBLL();

        //   [Route("product/{ProductCategoryFriendlyUrl?}")]
        //   public ActionResult Index(string ProductCategoryFriendlyUrl, int Page = 1)
        //   {
        //       ViewBag.Page = Page;
        //       ViewBag.ProductCategoryFriendlyUrl = ProductCategoryFriendlyUrl;

        ////       ViewBag.ProductContent = _ContentService.Get(Request.Url.Authority, "product");

        //       return View(_ProductService.Get(APP._SiteID
        //           , Request.Url.Authority
        //           , ProductCategoryFriendlyUrl
        //           , Page));
        //   }
        public PartialViewResult Index(string View, string ProductCategoryFriendlyUrl = null, int Page = 1)
        {
            var _P = _ProductService.Get(APP._SiteID
                , Request.Url.Authority
                , ProductCategoryFriendlyUrl
                , Page);
            return PartialView(View, _P);
        }
    }
}