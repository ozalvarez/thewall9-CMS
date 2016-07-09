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
    [RoutePrefix("api/media")]
    public class MediaController : ApiController
    {
        MediaBLL _MediaService = new MediaBLL();

        #region Customers
        public IHttpActionResult Get(int SiteID)
        {
            return Ok(_MediaService.Get(SiteID, User.Identity.GetUserId()));
        }
        public IHttpActionResult PostSave(FileRead Model)
        {
            var _M = _MediaService.SaveImage(Model, Model.SiteID, User.Identity.GetUserId());
            return Ok(_M);
        }
        public IHttpActionResult Delete(int MediaID, int SiteID)
        {
            _MediaService.DeleteMedia(MediaID, SiteID, User.Identity.GetUserId());
            return Ok();
        }
        #endregion
    }
}