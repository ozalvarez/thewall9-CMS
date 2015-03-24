using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    public class ProductBase
    {
        public int ProductID { get; set; }
        public int SiteID { get; set; }
        public string ProductAlias { get; set; }
    }
    public class ProductCultureBase
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string AdditionalInformation { get; set; }
        public string IconPath { get; set; }
        public string FriendlyUrl { get; set; }
    }

    public class ProductBinding : ProductBase
    {
        public List<ProductCultureBinding> ProductCultures { get; set; }
        public List<ProductTagBinding> ProductTags { get; set; }
        public List<ProductCategoryBinding> ProductCategories { get; set; }
        public List<ProductGalleryBinding> ProductGalleries { get; set; }
    }
    public class ProductCultureBinding : ProductCultureBase
    {
        public int CultureID { get; set; }
        public string CultureName { get; set; }
        public bool Adding { get; set; }
    }
    public class ProductTagBinding
    {
        public int ProductID { get; set; }
        public int TagID { get; set; }
        public string TagName { get; set; }
        public bool Adding { get; set; }
        public bool Deleting { get; set; }
    }
    public class ProductCategoryBinding
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryAlias { get; set; }
        public bool Adding { get; set; }
        public bool Deleting { get; set; }
    }
    public class ProductGalleryBase
    {
        public int ProductGalleryID { get; set; }
        public int ProductID { get; set; }
        public string PhotoPath { get; set; }

    }
    public class ProductGalleryBinding : ProductGalleryBase
    {
    }
}
