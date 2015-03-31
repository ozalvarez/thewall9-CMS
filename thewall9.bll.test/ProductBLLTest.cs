using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using thewall9.data.binding;
using System.Collections.Generic;
using thewall9.bll.Utils;
using thewall9.bll.Exceptions;

namespace thewall9.bll.test
{
    [TestClass]
    public class ProductBLLTest : BaseTest
    {
        private CategoryBinding _Category;
        private List<ProductCategoryBinding> _ProductCategories = new List<ProductCategoryBinding>();
        private List<ProductCurrencyBinding> _ProductCurrencies = new List<ProductCurrencyBinding>();
        private List<ProductCultureBinding> _ProductCultures = new List<ProductCultureBinding>();
        private ProductBinding _Product;

        private void SettingUp()
        {
            //INIT CULTURES
            _ProductCultures.Add(new ProductCultureBinding
            {
                CultureID = _Cultures[0].CultureID,
                ProductName = "lazos nazca para mujeres"
            });
            _ProductCultures.Add(new ProductCultureBinding
            {
                CultureID = _Cultures[1].CultureID,
                ProductName = "vows for girls"
            });
            //INIT CATEGORIES
            _Category = new CategoryBinding
            {
                CategoryAlias = "00",
                SiteID = _SiteID,
            };
            _Category.CategoryID = new CategoryBLL().Save(_Category, _CustomerUser.Id);
            _ProductCategories.Add(new ProductCategoryBinding
            {
                CategoryID = _Category.CategoryID,
                Adding = true
            });
            //INIT CURRENCIES
            var _C1 = new CurrencyBLL().Save(new CurrencyBinding
            {
                CurrencyName = "COP",
                SiteID = _SiteID
            }, _CustomerUser.Id);
            var _C2 = new CurrencyBLL().Save(new CurrencyBinding
            {
                CurrencyName = "USD",
                SiteID = _SiteID
            }, _CustomerUser.Id);
            _ProductCurrencies.Add(new ProductCurrencyBinding
            {
                CurrencyID = _C1,
                Price = 2600
            });
            _ProductCurrencies.Add(new ProductCurrencyBinding
            {
                CurrencyID = _C2,
                Price = 1
            });
            //INIT PRODUCT
            _Product = new data.binding.ProductBinding
            {
                ProductAlias = "00",
                SiteID = _SiteID,
                ProductCategories = _ProductCategories,
                ProductCurrencies = _ProductCurrencies,
                ProductCultures = _ProductCultures
            };
            _Product.ProductID = new ProductBLL().Save(_Product, _CustomerUser.Id);
            Assert.IsNotNull(_Product.ProductID);
        }
        [TestMethod]
        public void SaveProductTest()
        {
            SettingUp();
            var _P = new ProductBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsNotNull(_P);
            Assert.IsTrue(_P[0].ProductCategories.Count == 1);
            Assert.IsTrue(_P[0].ProductCurrencies.Count == 2);
        }
        [TestMethod]
        public void SaveProductIconTest()
        {
            SettingUp();

            var _FileName="logo.png";
            var _FileContent = "64base,"+System.Convert.ToBase64String(System.IO.File.ReadAllBytes(_FileName));
            
            _Product.ProductID = 0;
            _Product.ProductCultures[0].IconFile = new FileRead
            {
                FileContent = _FileContent,
                FileName = _FileName
            };
            _Product.ProductCultures[0].ProductName =_Product.ProductCultures[0].ProductName + DateTime.Now.ToShortTimeString();
            _Product.ProductCultures[1].ProductName = _Product.ProductCultures[1].ProductName + DateTime.Now.ToShortTimeString();
            _Product.ProductCultures[0].FriendlyUrl = null;
            _Product.ProductCultures[1].FriendlyUrl = null;


            var _PID=new ProductBLL().Save(_Product, _CustomerUser.Id);
            var _P = new ProductBLL().GetByID(_PID, _CustomerUser.Id);
            var _PathExpected=BaseBLL.StorageUrl+"/product-icon/"+_PID+"/"+_P.ProductCultures[0].CultureID+"/"+_FileName;
            Assert.IsTrue(_P.ProductCultures[0].IconPath==_PathExpected);
        }
        [TestMethod]
        public void GetTest()
        {
            SettingUp();
            var _P = new ProductBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsNotNull(_P);
        }
        [TestMethod]
        public void GetByIDTest()
        {
            SettingUp();
            var _P = new ProductBLL().GetByID(_Product.ProductID, _CustomerUser.Id);
            Assert.IsNotNull(_P);
        }
        [TestMethod]
        public void UpdateProductTest()
        {
            SettingUp();
            //CATEGORIES
            var _PC = new List<ProductCategoryBinding>();
            _PC.Add(new ProductCategoryBinding
            {
                CategoryID = _Category.CategoryID,
                Deleting = true
            });
            //CURRENCIES
            _ProductCurrencies[0].Price = 5000;
            _ProductCurrencies[1].Price = 2;
            //Updating
            _Product.ProductAlias = "00 Updated";
            new ProductBLL().Save(_Product, _CustomerUser.Id);
            var _P = new ProductBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsNotNull(_P);
            Assert.IsTrue(_P[0].ProductAlias == _Product.ProductAlias);
            Assert.IsTrue(_P[0].ProductCategories.Count == 1);
            Assert.IsTrue(_P[0].ProductCurrencies[0].Price == _ProductCurrencies[0].Price);
            Assert.IsTrue(_P[0].ProductCurrencies[1].Price == _ProductCurrencies[1].Price);
        }
        [TestMethod]
        public void SaveProductEmptyFriendlyURLTest()
        {
            SettingUp();
            var _P = new ProductBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsNotNull(_P);
            Assert.IsNotNull(_P[0].ProductCultures[0].FriendlyUrl);
        }
        [TestMethod]
        public void SaveProductDuplicateFriendlyURLTest()
        {
            SettingUp();
            _Product.ProductID = 0;
            _Product.ProductCultures[0].FriendlyUrl = _Product.ProductCultures[0].ProductName.CleanUrl();
            try
            {
                new ProductBLL().Save(_Product, _CustomerUser.Id);
                Assert.Fail();
            }
            catch (RuleException e)
            {
                Assert.IsTrue(e.CodeRuleException.Equals("0x001"));
            }
        }
        [TestMethod]
        public void SaveProductDuplicateFriendlyURLOnPostModelTest()
        {
            SettingUp();
            _Product.ProductID = 0;
            var _NewName = _Product.ProductCultures[0].ProductName.CleanUrl() + DateTime.Now.ToShortTimeString();
            _Product.ProductCultures[0].FriendlyUrl = _NewName;
            _Product.ProductCultures[1].FriendlyUrl = _NewName;
            try
            {
                new ProductBLL().Save(_Product, _CustomerUser.Id);
                Assert.Fail();
            }
            catch (RuleException e)
            {
                Assert.IsTrue(e.CodeRuleException.Equals("0x002"));
            }
        }
        [TestMethod]
        public void SaveProductDuplicateEmptyFriendlyURLOnPostModelTest()
        {
            SettingUp();
            _Product.ProductID = 0;
            var _NewName = _Product.ProductCultures[0].ProductName + DateTime.Now.ToShortTimeString();
            _Product.ProductCultures[0].ProductName = _NewName;
            _Product.ProductCultures[1].ProductName = _NewName;
            _Product.ProductCultures[0].FriendlyUrl = null;
            _Product.ProductCultures[1].FriendlyUrl = null;
            try
            {
                new ProductBLL().Save(_Product, _CustomerUser.Id);
                Assert.Fail();
            }
            catch (RuleException e)
            {
                Assert.IsTrue(e.CodeRuleException.Equals("0x002"));
            }
        }
        [TestMethod]
        public void GetWebTest()
        {
            SettingUp();
            var _P = new ProductBLL().Get(_SiteID, null, _Cultures[0].Name, _ProductCurrencies[0].CurrencyID,50,1);
            Assert.IsNotNull(_P);
        }
    }
}
