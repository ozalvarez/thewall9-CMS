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
    [RoutePrefix("api/currency")]
    public class CurrencyController : ApiController
    {
        CurrencyBLL _CurrencyService = new CurrencyBLL();

        public IHttpActionResult Get(int SiteID)
        {
            return Ok(_CurrencyService.Get(SiteID));
        }
        public IHttpActionResult PostSave(CurrencyBinding Model)
        {
            _CurrencyService.Save(Model, User.Identity.GetUserId());
            return Ok();
        }
        public IHttpActionResult Delete(int CurrencyID)
        {
            _CurrencyService.Delete(CurrencyID, User.Identity.GetUserId());
            return Ok();
        }
        [Route("default")]
        public IHttpActionResult PutDefault(int CurrencyID)
        {
            _CurrencyService.SetAsDefault(CurrencyID, User.Identity.GetUserId());
            return Ok();
        }
    }
}
