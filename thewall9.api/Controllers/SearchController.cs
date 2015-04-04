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
    [RoutePrefix("api/search")]
    public class SearchController : ApiController
    {
        ProductBLL _ProductService = new ProductBLL();

        #region Web
        [AllowAnonymous]
        public IHttpActionResult Get(int SiteID, string Lang, int CurrencyID, string Query, int Take, int Page)
        {
            return Ok(_ProductService.GetByQuery(SiteID, Lang, CurrencyID, Query, Take, Page));
        }
        #endregion
    }
}
