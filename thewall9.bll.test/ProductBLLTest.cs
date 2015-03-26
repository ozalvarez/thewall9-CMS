using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using thewall9.data.binding;
using System.Collections.Generic;

namespace thewall9.bll.test
{
    [TestClass]
    public class ProductBLLTest : BaseTest
    {
        private int _PID;
        private int _CID;
        private List<ProductCurrencyBinding> _C;

        private void SettingUp()
        {
            //INIT CATEGORIES
            _CID = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00",
                SiteID = _SiteID,
            }, _CustomerUser.Id);
            var _PC = new List<ProductCategoryBinding>();
            _PC.Add(new ProductCategoryBinding
            {
                CategoryID=_CID,
                Adding=true
            });
            //INIT CURRENCIES
            var _C1=new CurrencyBLL().Save(new CurrencyBinding{
                CurrencyName="COP",
                SiteID=_SiteID
            },_CustomerUser.Id);
            var _C2=new CurrencyBLL().Save(new CurrencyBinding{
                CurrencyName="USD",
                SiteID=_SiteID
            },_CustomerUser.Id);
            _C = new List<ProductCurrencyBinding>();
            _C.Add(new ProductCurrencyBinding
            {
                CurrencyID=_C1,
                Price=2600
            });
            _C.Add(new ProductCurrencyBinding
            {
                CurrencyID = _C2,
                Price = 1
            });
            //INIT PRODUCT
            _PID = new ProductBLL().Save(new data.binding.ProductBinding
            {
                ProductAlias = "00",
                SiteID = _SiteID,
                ProductCategories=_PC,
                ProductCurrencies=_C
            }, _CustomerUser.Id);
            Assert.IsNotNull(_PID);
        }
        [TestMethod]
        public void SaveProductTest()
        {
            SettingUp();
            var _P=new ProductBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsNotNull(_P);
            Assert.IsTrue(_P[0].ProductCategories.Count==1);
            Assert.IsTrue(_P[0].ProductCurrencies.Count == 2);
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
            var _P = new ProductBLL().GetByID(_PID, _CustomerUser.Id);
            Assert.IsNotNull(_P);
        }
        public void UpdateProductTest()
        {
            SettingUp();
            //CATEGORIES
            var _PC = new List<ProductCategoryBinding>();
            _PC.Add(new ProductCategoryBinding
            {
                CategoryID = _CID,
                Deleting = true
            });
            //CURRENCIES
            _C[0].Price = 5000;
            _C[1].Price = 2;
            //Updating
            _PID = new ProductBLL().Save(new data.binding.ProductBinding
            {
                ProductAlias = "00 Updated",
                SiteID = _SiteID,
                ProductCategories = _PC,
                ProductCurrencies = _C,
                ProductID = _PID
            }, _CustomerUser.Id);
            var _P = new ProductBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsNotNull(_P);
            Assert.IsTrue(_P[0].ProductAlias == "00 Updated");
            Assert.IsTrue(_P[0].ProductCategories.Count == 0);

            Assert.IsTrue(_P[0].ProductCurrencies[0].Price == _C[0].Price);
            Assert.IsTrue(_P[0].ProductCurrencies[1].Price == _C[1].Price);
        }
    }
}
