using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    public class CategoryBase
    {
        public int CategoryID { get; set; }
        public int SiteID { get; set; }
        public string CategoryAlias { get; set; }
        public int Priority { get; set; }
        public int CategoryParentID { get; set; }
        public string IconUrl { get; set; }
    }
    public class CategoryBinding : CategoryBase
    {
        public List<CategoryBinding> CategoryItems { get; set; }
        public List<CategoryCultureBinding> CategoryCultures { get; set; }
        public FileReadBinding Icon { get; set; }
    }

    public class CategoryCultureBase
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string FriendlyUrl { get; set; }
    }
    public class CategoryCultureBinding : CategoryCultureBase
    {
        public int CultureID { get; set; }
        public string CultureName { get; set; }
        public bool Adding { get; set; }
    }
    public class CategoryWeb : CategoryCultureBase
    {
        public int CategoryID { get; set; }
        public List<CategoryWeb> CategoryItems { get; set; }
        public string IconUrl { get; set; }
        public int CultureID { get; set; }
    }

    public class UpOrDown
    {
        public int CategoryID { get; set; }
        public bool Up { get; set; }
    }
}
