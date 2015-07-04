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
    [RoutePrefix("api/blog")]
    public class BlogController : ApiController
    {
        BlogBLL _BlogService = new BlogBLL();

        #region Web

        #endregion

        #region Customers
        public IHttpActionResult Get(int SiteID, int CultureID)
        {
            return Ok(_BlogService.Get(SiteID,CultureID));
        }
        [Route("byID")]
        public IHttpActionResult GetByID(int BlogPostID, int CultureID)
        {
            return Ok(_BlogService.GetDetail(BlogPostID, CultureID));
        }
        public IHttpActionResult PostSave(BlogPostModelBinding Model)
        {
            _BlogService.Save(Model, User.Identity.GetUserId());
            return Ok();
        }
        public IHttpActionResult Delete(int BlogPostID)
        {
            _BlogService.Delete(BlogPostID, User.Identity.GetUserId());
            return Ok();
        }

        //CATEGORIES
        [Route("category")]
        public IHttpActionResult GetCategories(int SiteID, int CultureID)
        {
            return Ok(_BlogService.GetCategories(SiteID, CultureID));
        }
        [Route("category")]
        public IHttpActionResult PostSaveCategory(BlogCategoryModelBinding Model)
        {
            _BlogService.SaveCategory(Model, User.Identity.GetUserId());
            return Ok();
        }
        [Route("category")]
        public IHttpActionResult DeleteCategory(int BlogCategoryID)
        {
            _BlogService.DeleteCategory(BlogCategoryID, User.Identity.GetUserId());
            return Ok();
        }

        //TAGS
        [Route("tag")]
        public IHttpActionResult GetTags(string Query)
        {
            return Ok(_BlogService.GetTags(Query));
        }
        #endregion
    }
}
