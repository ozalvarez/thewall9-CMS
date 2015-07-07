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

        #region Web
        [AllowAnonymous]
        public IHttpActionResult Get(int SiteID
            , string Url
            , string Lang
            , int CurrencyID
            , string ProductCategoryFriendlyUrl
            , int Page)
        {
            return Ok(_ProductService.Get(SiteID, Url, Lang, CurrencyID, ProductCategoryFriendlyUrl, Page));
        }
        [AllowAnonymous]
        public IHttpActionResult Get(int SiteID, string Url, string FriendlyUrl, int CurrencyID)
        {
            return Ok(_ProductService.Get(SiteID, Url, FriendlyUrl, CurrencyID));
        }
        #endregion

        #region Customers
        public IHttpActionResult Get(int SiteID)
        {
            return Ok(_ProductService.Get(SiteID, User.Identity.GetUserId()));
        }
        [Route("byID")]
        public IHttpActionResult GetByID(int ProductID)
        {
            return Ok(_ProductService.GetByID(ProductID, User.Identity.GetUserId()));
        }
        public IHttpActionResult PostSave(ProductBinding Model)
        {
            return Ok(_ProductService.Save(Model, User.Identity.GetUserId()));
        }
        [Route("gallery")]
        public IHttpActionResult DeleteGallery(int GalleryID)
        {
            _ProductService.DeleteGallery(GalleryID, User.Identity.GetUserId());
            return Ok();
        }
        [Route("move")]
        public IHttpActionResult PostMove(ProductUpdatePriorities Model)
        {
            _ProductService.UpdatePriorities(Model, User.Identity.GetUserId());
            return Ok();
        }
        public IHttpActionResult Delete(int ProductID)
        {
            _ProductService.Delete(ProductID, User.Identity.GetUserId());
            return Ok();
        }
        [Route("categories")]
        public IHttpActionResult GetCategories(int SiteID, string Query)
        {
            return Ok(_ProductService.GetCategories(SiteID, Query));
        }
        [Route("tags")]
        public IHttpActionResult GetTags(int SiteID, string Query)
        {
            return Ok(_ProductService.GetTags(SiteID, Query));
        }
        #endregion
    }
}
