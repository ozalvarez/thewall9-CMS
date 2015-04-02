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
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        OrderBLL _OrderService = new OrderBLL();

        public IHttpActionResult Get(int SiteID)
        {
            return Ok(_OrderService.Get(SiteID));
        }
        [AllowAnonymous]
        public IHttpActionResult PostSave(OrderBinding Model)
        {
            _OrderService.Save(Model);
            return Ok();
        }
        public IHttpActionResult Delete(int OrderID)
        {
            _OrderService.Delete(OrderID, User.Identity.GetUserId());
            return Ok();
        }
    }
}
