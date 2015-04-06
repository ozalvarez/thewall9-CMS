using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using thewall9.bll;
using Microsoft.AspNet.Identity;
using thewall9.data.binding;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace thewall9.api.Controllers
{

    [Authorize]
    [RoutePrefix("api/product-gallery")]
    public class ProductGalleryUploadController : ApiController
    {
        ProductBLL _ProductService = new ProductBLL();

        #region Customers
     
        [Route("upload")]
        [HttpPost]
        public async Task<IHttpActionResult> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);

            var uploadFolder = "~/App_Data/Tmp/FileUploads";
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            var provider = new MultipartFormDataStreamProvider(root);

            var result = await Request.Content.ReadAsMultipartAsync(provider);

            var _ProductID = Convert.ToInt32(result.FormData["ProductID"]);
            var _FileName = JsonConvert.DeserializeObject(result.FileData.First().Headers.ContentDisposition.FileName).ToString();
            var _TempPath = result.FileData.First().LocalFileName;
            //SAVE FILE
            return Ok(_ProductService.AddGallery(_ProductID, _TempPath, _FileName, User.Identity.GetUserId()));
        }
        #endregion
    }
}
