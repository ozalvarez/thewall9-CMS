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
    [RoutePrefix("api/page")]
    public class PageController : ApiController
    {
        PageBLL _PageService = new PageBLL();

        #region WEB
        [AllowAnonymous]
        public IHttpActionResult Get(int SiteID, string Url, string FriendlyUrl)
        {
            return Ok(_PageService.GetPage(SiteID, Url, FriendlyUrl));
        }
        [AllowAnonymous]
        public IHttpActionResult GetByAlias(int SiteID, string Url, string Alias, string Lang)
        {
            return Ok(_PageService.GetPageByAlias(SiteID, Url, Alias, Lang));
        }
        [AllowAnonymous]
        [Route("menu")]
        public IHttpActionResult GetMenu(int SiteID, string Url, string DefaultLang)
        {
            return Ok(_PageService.GetMenu(SiteID, Url, DefaultLang));
        }
        [AllowAnonymous]
        [Route("sitemap")]
        public IHttpActionResult GetSitemap(int SiteID, string Url)
        {
            return Ok(_PageService.GetSitemap(SiteID, Url));
        }
        [AllowAnonymous]
        public IHttpActionResult GetPageFriendlyUrl(int SiteID, string Url, string FriendlyUrl, string TargetLang)
        {
            return Ok(_PageService.GetPageFriendlyUrl(SiteID, Url, FriendlyUrl, TargetLang));
        }
        #endregion

        #region CUSTOMER
        public IHttpActionResult Get(int SiteID)
        {
            return Ok(_PageService.Get(SiteID, User.Identity.GetUserId()));
        }
        public IHttpActionResult Get(int PageID, int CultureID)
        {
            return Ok(_PageService.Get(PageID, CultureID, User.Identity.GetUserId()));
        }
        public IHttpActionResult PostSave(PageBinding Model)
        {
            return Ok(_PageService.Save(Model, User.Identity.GetUserId()));
        }
        [Route("save-culture")]
        public IHttpActionResult SaveCulture(PageCultureBinding Model)
        {
            _PageService.SaveCulture(Model, User.Identity.GetUserId());
            return Ok();
        }
        public IHttpActionResult Delete(int PageID)
        {
            _PageService.Delete(PageID, User.Identity.GetUserId());
            return Ok();
        }
        [Route("move")]
        public IHttpActionResult Move(MoveBinding model)
        {

            _PageService.Move(model, User.Identity.GetUserId());
            return Ok();
        }
        [Route("in-menu")]
        public IHttpActionResult InMenu(PublishBinding Model)
        {
            _PageService.InMenu(Model, User.Identity.GetUserId());
            return Ok();
        }
        #endregion
    }
}
