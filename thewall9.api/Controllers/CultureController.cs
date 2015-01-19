using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using thewall9.bll;

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
    }
}
