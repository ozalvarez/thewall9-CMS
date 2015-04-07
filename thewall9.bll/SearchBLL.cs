using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using thewall9.bll.Exceptions;
using thewall9.data;
using thewall9.data.binding;
using thewall9.data.Models;

namespace thewall9.bll
{
    public class SearchBLL : BaseBLL
    {
        public SearchBinding Get(int SiteID, string Lang, int CurencyID, string Query, int Take, int Page)
        {
            return new SearchBinding
            {
                Products = new ProductBLL().GetByQuery(SiteID, Lang, CurencyID, Query, Take, Page)
            };
        }
    }
}
