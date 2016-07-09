using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using thewall9.data.binding;
using Newtonsoft.Json;
using Elmah;

namespace thewall9.web.parent.BLL
{
    public class CategoryBLL : BaseBLL
    {
        public CategoryWeb GetByID(int CategoryID
            , string FriendlyUrl)
        {
            return DownloadObject<CategoryWeb>("api/category?CategoryID=" + CategoryID
                + "&FriendlyUrl=" + FriendlyUrl);
        }
    }
}
