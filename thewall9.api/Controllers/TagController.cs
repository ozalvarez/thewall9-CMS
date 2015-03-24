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
    [RoutePrefix("api/tag")]
    public class TagController : ApiController
    {
        TagBLL _TagService = new TagBLL();

        public IHttpActionResult Get(int SiteID)
        {
            return Ok(_TagService.Get(SiteID));
        }
        public IHttpActionResult PostSave(TagBinding Model)
        {
            _TagService.Save(Model, User.Identity.GetUserId());
            return Ok();
        }
        public IHttpActionResult Delete(int TagID)
        {
            _TagService.Delete(TagID, User.Identity.GetUserId());
            return Ok();
        }
    }
}
