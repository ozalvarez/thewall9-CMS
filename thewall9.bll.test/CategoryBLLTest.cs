using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace thewall9.bll.test
{
    [TestClass]
    public class CategoryBLLTest : BaseTest
    {
        private void SettingUp()
        {
            var _CID = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00",
                SiteID = _SiteID,
            }, _CustomerUser.Id);
            var _CID2 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00 - 00",
                SiteID = _SiteID,
                CategoryParentID = _CID
            }, _CustomerUser.Id);
            var _CID3 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00 - 01",
                SiteID = _SiteID,
                CategoryParentID = _CID
            }, _CustomerUser.Id);
            var _CID4 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00 - 02",
                SiteID = _SiteID,
                CategoryParentID = _CID
            }, _CustomerUser.Id);
            var _CID5 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "01",
                SiteID = _SiteID,
            }, _CustomerUser.Id);
            Assert.IsNotNull(_CID);
        }
        [TestMethod]
        public void SaveCategoryTest()
        {
            SettingUp();
        }
        [TestMethod]
        public void GetCategoriesTreeTest()
        {
            SettingUp();
            var _Site=new CategoryBLL().Get(_SiteID);
            Assert.IsTrue(_Site.Count == 2);
            Assert.IsTrue(_Site[0].CategoryItems.Count == 3);
            Assert.IsTrue(_Site[1].Priority == 1);
            Assert.IsTrue(_Site[0].CategoryItems[2].Priority == 2);
        }
    }
}
