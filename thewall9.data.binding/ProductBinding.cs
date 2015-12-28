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
        public int Priority { get; set; }
        public bool Enabled { get; set; }
        public bool New { get; set; }
        public bool Featured { get; set; }
    }
    public class ProductBinding : ProductBase
    {
        /// <summary>
        /// FIRST PRODUCT CULTURE NAME
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// FIRST PRODUCT CULTURE ICON PATH
        /// </summary>
        public string IconPath { get; set; }

        public List<ProductCultureBinding> ProductCultures { get; set; }
        public List<ProductTagBinding> ProductTags { get; set; }
        public List<ProductCategoryBinding> ProductCategories { get; set; }
        public List<ProductGalleryBinding> ProductGalleries { get; set; }
        public List<ProductCurrencyBinding> ProductCurrencies { get; set; }
    }

    public class ProductCultureBase
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string AdditionalInformation { get; set; }
        public string IconPath { get; set; }
        public string FriendlyUrl { get; set; }
    }
    public class ProductCultureBinding : ProductCultureBase
    {
        public int CultureID { get; set; }
        public string CultureName { get; set; }
        public bool Adding { get; set; }
        public FileRead IconFile { get; set; }
    }
    public class ProductWeb : ProductCultureBase
    {
        public int ProductID { get; set; }
        public double Price { get; set; }
        public double PriceOld { get; set; }
        public string CultureName { get; set; }
        public List<ProductGalleryBinding> Galleries { get; set; }
        public bool New { get; set; }
        public bool Featured { get; set; }
        public bool Sale { get; set; }
    }
    public class ProductsWeb
    {
        public List<ProductWeb> Products { get; set; }
        public List<CategoryWeb> Categories { get; set; }
        public int NumberPages { get; set; }
        public int CultureID { get; set; }
        public string CultureName { get; set; }
        public CategoryWeb Category { get; set; }
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
    public class ProductCurrencyBase
    {
        public double Price { get; set; }
        public double PriceOld { get; set; }
    }
    public class ProductCurrencyBinding : ProductCurrencyBase
    {
        public int ProductID { get; set; }
        public int CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public bool Adding { get; set; }
    }
    public class ProductUpdatePriorities
    {
        public int ProductID { get; set; }
        public int Index { get; set; }
    }
    public enum ProductBooleanType
    {
        Enable = 0, New = 1, Featured = 2
    }
    public class ProductBoolean
    {
        public int ProductID { get; set; }
        public bool Boolean { get; set; }
        public ProductBooleanType ProductBooleanType { get; set; }
    }
}
