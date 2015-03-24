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

        private void SettingUp()
        {
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
            _PID = new ProductBLL().Save(new data.binding.ProductBinding
            {
                ProductAlias = "00",
                SiteID = _SiteID,
                ProductCategories=_PC,
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
        }
        [TestMethod]
        public void UpdateProductTest()
        {
            SettingUp();
            var _PC = new List<ProductCategoryBinding>();
            _PC.Add(new ProductCategoryBinding
            {
                CategoryID = _CID,
                Deleting = true
            });
            _PID = new ProductBLL().Save(new data.binding.ProductBinding
            {
                ProductAlias = "00 Updated",
                SiteID = _SiteID,
                ProductCategories = _PC,
                ProductID=_PID
            }, _CustomerUser.Id);
            var _P = new ProductBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsNotNull(_P);
            Assert.IsTrue(_P[0].ProductAlias == "00 Updated");
            Assert.IsTrue(_P[0].ProductCategories.Count == 0);
        }
    }
}
