using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using thewall9.bll;
using Microsoft.AspNet.Identity;
using thewall9.data;
using thewall9.data.binding;
using thewall9.bll.Exceptions;
using Newtonsoft.Json;
using System.IO;
using thewall9.bll.Utils;

namespace thewall9.api.Controllers
{
    [Authorize]
    [RoutePrefix("api/site")]
    public class SiteController : ApiController
    {
        SiteBLL SiteService = new SiteBLL();

        #region WEB
        [AllowAnonymous]
        public IHttpActionResult Get(int SiteID, string Url, string Lang)
        {
            return Ok(SiteService.Get(SiteID, Url, Lang));
        }
        #endregion

        #region CUSTOMERS
        public IHttpActionResult Get()
        {
            return Ok(SiteService.Get(User.Identity.GetUserId()));
        }
        public IHttpActionResult PutSaveSite(SiteBinding Model)
        {
            SiteService.Save(Model, User.Identity.GetUserId());
            return Ok();
        }
        #endregion

        #region ADMIN
        [Authorize(Roles = "admin")]
        [Route("all")]
        public IHttpActionResult GetAll()
        {
            return Ok(SiteService.GetAll());
        }
        [Authorize(Roles = "admin")]
        public IHttpActionResult PostSave(SiteAllBinding Model)
        {
            SiteService.Save(Model);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        public IHttpActionResult Delete(int SiteID)
        {
            SiteService.Delete(SiteID);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [Route("enable")]
        public IHttpActionResult PostEnable(SiteEnabledBinding Model)
        {
            SiteService.Enable(Model);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [Route("user")]
        public IHttpActionResult PostAddUser(AddUserInSiteBinding Model)
        {
            SiteService.AddUserToAllRoles(Model);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [Route("users")]
        public IHttpActionResult GetUsers(int SiteID)
        {
            return Ok(SiteService.GetUsers(SiteID));
        }
        [Authorize(Roles = "admin")]
        [Route("rol")]
        public IHttpActionResult PostAddRol(SiteAddRol Model)
        {
            SiteService.AddUserToRol(Model);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [Route("user")]
        public IHttpActionResult DeleteUser(int SiteID, String UserID)
        {
            SiteService.DeleteUser(UserID, SiteID);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [Route("export")]
        public IHttpActionResult GetExport(int SiteID)
        {
            return Ok(new { Url = SiteService.Export(SiteID) });
        }
        [Authorize(Roles = "admin")]
        [Route("import")]
        public IHttpActionResult Import(FileRead _Model)
        {
            SiteService.Import(_Model);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [Route("duplicate")]
        public IHttpActionResult Duplicate([FromBody]int SiteID)
        {
            SiteService.Duplicate(SiteID);
            return Ok();
        }
        #endregion
    }
}
