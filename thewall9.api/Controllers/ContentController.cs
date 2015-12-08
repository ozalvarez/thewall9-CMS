using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using thewall9.bll;
using thewall9.bll.Exceptions;
using Microsoft.AspNet.Identity;
using thewall9.data;
using thewall9.data.binding;

namespace thewall9.api.Controllers
{
    [Authorize]
    [RoutePrefix("api/content")]
    public class ContentController : ApiController
    {
        ContentBLL _ContentService = new ContentBLL();

        #region WEB
        [AllowAnonymous]
        public IHttpActionResult Get(int SiteID, string Url, string AliasList, string Lang)
        {
           //  return Ok(_ContentService.GetContent(SiteID, Url, AliasList, Lang));
            return Ok(_ContentService.GetByRoot(SiteID, Url, AliasList, Lang));
        }
        #endregion

        #region CUSTOMER
        public IHttpActionResult GetTree(int SiteID, int CultureID)
        {
            return Ok(_ContentService.GetTree(SiteID, CultureID, User.Identity.GetUserId()));
        }
        [Route("menu")]
        public IHttpActionResult GetMenu(int SiteID, int CultureID)
        {
            return Ok(_ContentService.GetMenu(SiteID, CultureID, User.Identity.GetUserId()));
        }
        [Route("property")]
        public IHttpActionResult GetTreeByContentProperty(int ContentPropertyID, int CultureID)
        {
            return Ok(_ContentService.GetTreeByContentProperty(ContentPropertyID, CultureID, User.Identity.GetUserId()));
        }
        [Route("save-tree")]
        public IHttpActionResult PostSaveTree(ContentTreeBinding Model)
        {
            _ContentService.SaveTree(Model, User.Identity.GetUserId());
            return Ok();
        }
        #endregion  

        [Route("export")]
        public IHttpActionResult GetExport(int SiteID, int ContentPropertyID)
        {
            return Ok(new
            {
                Url = _ContentService.Export(ContentPropertyID, SiteID, User.Identity.GetUserId())
            });
        }
        [Route("import")]
        public IHttpActionResult PostImport(ImportBinding Model)
        {
            _ContentService.Import(Model, User.Identity.GetUserId());
            return Ok();
        }

        public IHttpActionResult PostSave(ContentBinding Model)
        {
            return Ok(_ContentService.Save(Model, User.Identity.GetUserId()));
        }

        public IHttpActionResult Get(int SiteID)
        {
            return Ok(_ContentService.Get(SiteID, User.Identity.GetUserId()));
        }

        [Route("duplicate")]
        public IHttpActionResult Duplicate(ContentBinding Model)
        {
            return Ok(_ContentService.Duplicate(Model, User.Identity.GetUserId()));
        }
        [Route("duplicate-tree")]
        public IHttpActionResult DuplicateTree(ContentTree Model)
        {
            return Ok(_ContentService.DuplicateTree(Model, User.Identity.GetUserId()));
        }
        public IHttpActionResult Delete(int ContentPropertyID)
        {
            _ContentService.Delete(ContentPropertyID, User.Identity.GetUserId());
            return Ok();
        }
        [Route("move")]
        public IHttpActionResult Move(ContentMoveBinding model)
        {
            _ContentService.Move(model, User.Identity.GetUserId());
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [Route("lock")]
        public IHttpActionResult Lock(ContentBoolean Model)
        {
            _ContentService.Lock(Model, User.Identity.GetUserId());
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [Route("enable")]
        public IHttpActionResult Enabled(ContentBoolean Model)
        {
            _ContentService.Enable(Model, User.Identity.GetUserId());
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [Route("lock-all")]
        public IHttpActionResult Lock([FromBody]int SiteID)
        {
            _ContentService.LockAll(SiteID, User.Identity.GetUserId());
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [Route("show-in-content")]
        public IHttpActionResult ShowInContent(ContentBoolean Model)
        {
            _ContentService.ShowInContent(Model, User.Identity.GetUserId());
            return Ok();
        }
        [Route("inmenu")]
        public IHttpActionResult InMenu(ContentBoolean Model)
        {
            _ContentService.InMenu(Model, User.Identity.GetUserId());
            return Ok();
        }
    }
}
