using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using thewall9.bll;
using Microsoft.AspNet.Identity;
using thewall9.data.binding;
namespace thewall9.api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        ProductBLL _ProductService = new ProductBLL();

        public IHttpActionResult Get(int SiteID)
        {
            return Ok(_ProductService.Get(SiteID, User.Identity.GetUserId()));
        }
        public IHttpActionResult PostSave(ProductBinding Model)
        {
            return Ok(_ProductService.Save(Model, User.Identity.GetUserId()));
        }
        public IHttpActionResult Delete(int ProductID)
        {
            _ProductService.Delete(ProductID, User.Identity.GetUserId());
            return Ok();
        }
        [AllowAnonymous]
        [Route("categories")]
        public IHttpActionResult GetCategories(int SiteID, string Query)
        {
            return Ok(_ProductService.GetCategories(SiteID, Query));
        }
    }
}
