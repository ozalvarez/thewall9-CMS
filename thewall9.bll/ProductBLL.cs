using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.bll.Exceptions;
using thewall9.data;
using thewall9.data.binding;
using thewall9.data.Models;

namespace thewall9.bll
{
    public class ProductBLL : BaseBLL
    {
        private Product GetByID(int ProductID, ApplicationDbContext _c)
        {
            return _c.Products.Where(m => m.ProductID == ProductID).SingleOrDefault();
        }
        public List<ProductBinding> Get(int SiteID, string UserID)
        {
            using (var _c = db)
            {
                Can(SiteID, UserID, _c);
                return (from c in _c.Products
                        where c.SiteID == SiteID
                        select new ProductBinding
                        {
                            ProductID = c.ProductID,
                            ProductAlias = c.ProductAlias,
                            SiteID = c.SiteID,
                            ProductCultures = c.ProductCultures.Select(m => new ProductCultureBinding
                            {
                                ProductName = m.ProductName,
                                CultureID = m.CultureID,
                                CultureName = m.Culture.Name,
                                Description = m.Description,
                                AdditionalInformation = m.AdditionalInformation,
                                IconPath = m.IconPath,
                                FriendlyUrl = m.FriendlyUrl
                            }).ToList(),
                            ProductTags = c.ProductTags.Select(m => new ProductTagBinding
                            {
                                ProductID = m.ProductID,
                                TagID = m.TagID,
                                TagName = m.Tag.TagName
                            }).ToList(),
                            ProductCategories = c.ProductCategories.Select(m => new ProductCategoryBinding
                            {
                                ProductID = m.ProductID,
                                CategoryID = m.CategoryID,
                                CategoryAlias = m.Category.CategoryAlias
                            }).ToList(),
                            ProductGalleries = c.ProductGalleries.Select(m => new ProductGalleryBinding
                            {
                                ProductID = m.ProductID,
                                ProductGalleryID = m.ProductGalleryID,
                                PhotoPath = m.PhotoPath
                            }).ToList(),
                        }).ToList();
            }
        }

        public int Save(ProductBinding Model, string UserID)
        {
            using (var _c = db)
            {
                Can(Model.SiteID, UserID, _c);
                if (Model.ProductCategories == null || Model.ProductCategories.Count == 0)
                    throw new RuleException("Categories Empty", "0x000");
                var _Product = new Product();
                if (Model.ProductID == 0)
                {
                    //CREATING
                    _Product.SiteID = Model.SiteID;
                    _Product.ProductCultures = new List<ProductCulture>();
                    //ADDING CULTURES
                    if (Model.ProductCultures != null)
                    {
                        foreach (var m in Model.ProductCultures)
                        {
                            _Product.ProductCultures.Add(new ProductCulture
                            {
                                ProductName = m.ProductName,
                                CultureID = m.CultureID,
                                Description = m.Description,
                                AdditionalInformation = m.AdditionalInformation,
                                IconPath = m.IconPath,
                                FriendlyUrl = m.FriendlyUrl
                            });
                        }
                    }
                    _c.Products.Add(_Product);
                }
                else
                {
                    //UPDATING
                    _Product = GetByID(Model.ProductID, _c);
                    //ADDING CULTURES
                    if (Model.ProductCultures != null)
                    {
                        foreach (var item in Model.ProductCultures)
                        {
                            if (item.Adding)
                            {
                                _Product.ProductCultures.Add(new ProductCulture
                                {
                                    ProductName = item.ProductName,
                                    CultureID = item.CultureID,
                                    Description = item.Description,
                                    AdditionalInformation = item.AdditionalInformation,
                                    IconPath = item.IconPath,
                                    FriendlyUrl = item.FriendlyUrl
                                });
                            }
                            else
                            {
                                var _CC = _Product.ProductCultures.Where(m => m.CultureID == item.CultureID).SingleOrDefault();
                                _CC.ProductName = item.ProductName;
                                _CC.Description = item.Description;
                                _CC.AdditionalInformation = item.AdditionalInformation;
                                _CC.IconPath = item.IconPath;
                                _CC.FriendlyUrl = item.FriendlyUrl;
                            }
                        }
                    }
                }
                _Product.ProductAlias = Model.ProductAlias;
                _Product.ProductCategories = new List<ProductCategory>();
                //ADDING CATEGORIES
                foreach (var item in Model.ProductCategories)
                {
                    ProductCategory _PC = null;
                    if (Model.ProductID != 0)
                        _PC = _c.ProductCategories.Where(m => m.CategoryID == item.CategoryID && m.ProductID == Model.ProductID).SingleOrDefault();
                    if (item.Adding || Model.ProductID == 0)
                    {
                        if (_PC == null)
                            _Product.ProductCategories.Add(new ProductCategory
                            {
                                CategoryID = item.CategoryID
                            });
                    }
                    else if (item.Deleting)
                    {
                        if (_PC != null)
                            _Product.ProductCategories.Remove(_PC);
                    }
                }
                //ADDING TAGS
                if (Model.ProductTags != null)
                {
                    foreach (var item in Model.ProductTags)
                    {
                        ProductTag _PT = null;
                        if (Model.ProductID != 0)
                            _PT = _c.ProductTags.Where(m => m.TagID == item.TagID && m.ProductID == Model.ProductID).SingleOrDefault();
                        if (item.Adding || Model.ProductID == 0)
                        {
                            if (_PT == null)
                                _Product.ProductTags.Add(new ProductTag
                                {
                                    TagID = item.TagID
                                });
                        }
                        else if (item.Deleting)
                        {
                            if (_PT != null)
                                _Product.ProductTags.Remove(_PT);
                        }
                    }
                }
                _c.SaveChanges();
                return _Product.ProductID;
            }
        }
        public void Delete(int ProductID, string UserID)
        {
            using (var _c = db)
            {
                var _Category = GetByID(ProductID, _c);
                Can(_Category.SiteID, UserID, _c);
                _c.Products.Remove(_Category);
                _c.SaveChanges();
            }
        }

        //CATEGORIES
        public List<ProductCategoryAutoCompleteBinding> GetCategories(int SiteID, string Query)
        {
            using (var _c = db)
            {
                return (from cc in _c.CategoryCultures
                        where cc.CategoryName.ToLower().Contains(Query.ToLower())
                        || cc.Category.CategoryAlias.ToLower().Contains(Query.ToLower())
                        select new ProductCategoryAutoCompleteBinding
                        {
                            CategoryID = cc.CategoryID,
                            CategoryAlias = cc.Category.CategoryAlias
                        }).Distinct().ToList();
            }
        }
    }
}
