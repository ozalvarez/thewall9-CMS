using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data.binding;

namespace thewall9.data
{
    public class Brand : BrandBase
    {
        [ForeignKey("SiteID")]
        public virtual Site Site { get; set; }

        public Brand() { }
        public Brand(BrandBase Model) : base(Model) { }
        public virtual List<Product> Products { get; set; }
}
    //public class BrandProduct
    //{
    //    public int BrandID { get; set; }
    //    public int ProductID { get; set; }
    //    [ForeignKey("BrandID")]
    //    public virtual Brand Brand { get; set; }
    //    [ForeignKey("ProductID")]
    //    public virtual Product Product { get; set; }
    //}
}
