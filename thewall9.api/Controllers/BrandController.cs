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
    [RoutePrefix("api/brand")]
    public class BrandController : ApiController
    {
        BrandBLL _BrandService = new BrandBLL();

        public IHttpActionResult Get(int SiteID)
        {
            return Ok(_BrandService.Get(SiteID));
        }
        public IHttpActionResult PostSave(BrandBinding Model)
        {
            _BrandService.Save(Model, User.Identity.GetUserId());
            return Ok();
        }
        public IHttpActionResult Delete(int BrandID)
        {
            _BrandService.Delete(BrandID, User.Identity.GetUserId());
            return Ok();
        }
    }
}
