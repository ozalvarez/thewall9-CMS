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
    [RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {
        CategoryBLL _CategoryService = new CategoryBLL();

        public IHttpActionResult Get(int SiteID)
        {
            return Ok(_CategoryService.Get(SiteID, User.Identity.GetUserId()));
        }
        public IHttpActionResult PostSave(CategoryBinding Model)
        {
            return Ok(_CategoryService.Save(Model, User.Identity.GetUserId()));
        }
        public IHttpActionResult Delete(int CategoryID)
        {
            _CategoryService.Delete(CategoryID, User.Identity.GetUserId());
            return Ok();
        }
        [Route("up-or-down")]
        public IHttpActionResult PostUpOrDown(UpOrDown Model)
        {
            _CategoryService.UpOrDown(Model, User.Identity.GetUserId());
            return Ok();
        }
    }
}
