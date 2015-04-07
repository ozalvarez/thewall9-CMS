using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using thewall9.bll;
using thewall9.data.binding;
using Microsoft.AspNet.Identity;

namespace thewall9.api.Controllers
{
    [Authorize]
    [RoutePrefix("api/culture")]
    public class CultureController : ApiController
    {
        CultureBLL _CultureService = new CultureBLL();
        public IHttpActionResult Get(int SiteID)
        {
            return Ok(_CultureService.Get(SiteID));
        }
        public IHttpActionResult Save(CultureBinding Model)
        {
            _CultureService.Save(Model, User.Identity.GetUserId());
            return Ok();
        }
    }
}
