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
    public class Product : ProductBase
    {
        [ForeignKey("SiteID")]
        public virtual Site Site { get; set; }
        public virtual List<ProductCulture> ProductCultures { get; set; }
        public virtual List<ProductTag> ProductTags { get; set; }
        public virtual List<ProductCategory> ProductCategories { get; set; }
        public virtual List<ProductGallery> ProductGalleries { get; set; }
        public virtual List<ProductCurrency> ProductCurrencies { get; set; }
        public virtual Brand Brand { get; set; }
    }
    public class ProductCulture : ProductCultureBase
    {
        [Key, Column(Order = 1)]
        public int ProductID { get; set; }
        [Key, Column(Order = 2)]
        public int CultureID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        [ForeignKey("CultureID")]
        public virtual Culture Culture { get; set; }
    }
    public class ProductCategory : ProductCategoryBase
    {
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
    }
    public class ProductTag
    {
        [Key, Column(Order = 1)]
        public int ProductID { get; set; }
        [Key, Column(Order = 2)]
        public int TagID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        [ForeignKey("TagID")]
        public virtual Tag Tag { get; set; }
    }
    public class ProductGallery : ProductGalleryBase
    {
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        public ProductGallery() { }
        public ProductGallery(int ProductID, string Path)
        {
            this.ProductID = ProductID;
            this.PhotoPath = Path;
        }
    }
    public class ProductCurrency : ProductCurrencyBase
    {
        [Key, Column(Order = 1)]
        public int ProductID { get; set; }
        [Key, Column(Order = 2)]
        public int CurrencyID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        [ForeignKey("CurrencyID")]
        public virtual Currency Currency { get; set; }
    }
}
