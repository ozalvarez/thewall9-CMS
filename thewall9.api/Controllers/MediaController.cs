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

        public IHttpActionResult PostSave(FileReadSite Model)
        {
            return Ok(_MediaService.SaveImage(Model, User.Identity.GetUserId()));
        }
        #endregion
    }
}