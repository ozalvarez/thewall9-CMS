using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    public class FileRead : MediaBase
    {
        public string FileContent { get; set; }
        public string FileName { get; set; }
        public bool Deleting { get; set; }
        public bool Adding { get; set; }
    }
    public class FileReadSite : FileRead
    {
        public int SiteID { get; set; }
    }
    public class SiteMapModel
    {
        public bool Ecommerce { get; set; }
        public List<PageCultureBinding> Pages { get; set; }
        public List<ProductWeb> Products { get; set; }
        public List<CategoryWeb> Categories { get; set; }
    }
}
