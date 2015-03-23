using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data.binding;

namespace thewall9.data
{
    public class Category : CategoryBase
    {
        [ForeignKey("SiteID")]
        public virtual Site Site { get; set; }
        public virtual List<CategoryCulture> CategoryCultures { get; set; }
    }
    public class CategoryCulture : CategoryCultureBase
    {
        [Key, Column(Order = 1)]
        public int CategoryID { get; set; }
        [Key, Column(Order = 2)]
        public int CultureID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
        [ForeignKey("CultureID")]
        public virtual Culture Culture { get; set; }
    }
}
