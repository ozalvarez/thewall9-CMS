using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using thewall9.data.binding;

namespace thewall9.bll.test
{
    [TestClass]
    public class CategoryBLLTest : BaseTest
    {
        private int _CID;
        private int _CID2;
        private int _CID3;
        private int _CID4;
        private int _CID5;
        private int _CID6;
        private int _CID7;
        private int _CID8;
        private void SettingUp()
        {
            _CID = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00",
                SiteID = _SiteID,
            }, _CustomerUser.Id);
            _CID2 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00 - 00",
                SiteID = _SiteID,
                CategoryParentID = _CID
            }, _CustomerUser.Id);
            _CID3 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00 - 01",
                SiteID = _SiteID,
                CategoryParentID = _CID
            }, _CustomerUser.Id);
            _CID4 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00 - 02",
                SiteID = _SiteID,
                CategoryParentID = _CID
            }, _CustomerUser.Id);
            _CID6 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00 - 03",
                SiteID = _SiteID,
                CategoryParentID = _CID
            }, _CustomerUser.Id);
            _CID8 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00 - 03 - 00",
                SiteID = _SiteID,
                CategoryParentID = _CID6
            }, _CustomerUser.Id);
            _CID5 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "01",
                SiteID = _SiteID,
            }, _CustomerUser.Id);
            Assert.IsNotNull(_CID);
            _CID7 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "01 - 01",
                SiteID = _SiteID,
                CategoryParentID = _CID5 
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
            var _Site = new CategoryBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsTrue(_Site.Count == 2);
            Assert.IsTrue(_Site[0].CategoryItems.Count == 4);
            Assert.IsTrue(_Site[1].Priority == 1);
            Assert.IsTrue(_Site[0].CategoryItems[2].Priority == 2);
            Assert.IsTrue(_Site[0].CategoryItems[3].CategoryItems.Count == 1);
        }
        [TestMethod]
        public void UpTest()
        {
            SettingUp();
            new CategoryBLL().UpOrDown(new UpOrDown{
                CategoryID=_CID4,
                Up=true
            },_CustomerUser.Id);
            var _Site = new CategoryBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsTrue(_Site[0].CategoryItems[1].CategoryID == _CID4);
            Assert.IsTrue(_Site[0].CategoryItems[1].Priority == 1);
            Assert.IsTrue(_Site[0].CategoryItems[2].CategoryID == _CID3);
            Assert.IsTrue(_Site[0].CategoryItems[2].Priority == 2);
        }
        [TestMethod]
        public void UpFirstItemTest()
        {
            SettingUp();
            new CategoryBLL().UpOrDown(new UpOrDown
            {
                CategoryID = _CID,
                Up = true
            }, _CustomerUser.Id);
            var _Site = new CategoryBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsTrue(_Site[0].CategoryID == _CID);
            Assert.IsTrue(_Site[0].Priority == 0);
        }
        public void DownTest()
        {
            SettingUp();
            new CategoryBLL().UpOrDown(new UpOrDown
            {
                CategoryID = _CID4,
                Up = false
            }, _CustomerUser.Id);
            var _Site = new CategoryBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsTrue(_Site[0].CategoryItems[3].CategoryID == _CID4);
            Assert.IsTrue(_Site[0].CategoryItems[3].Priority == 3);
            Assert.IsTrue(_Site[0].CategoryItems[2].CategoryID == _CID6);
            Assert.IsTrue(_Site[0].CategoryItems[2].Priority == 2);
        }
        [TestMethod]
        public void DownLastItemTest()
        {
            SettingUp();
            new CategoryBLL().UpOrDown(new UpOrDown
            {
                CategoryID = _CID5,
                Up = false
            }, _CustomerUser.Id);
            var _Site = new CategoryBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsTrue(_Site[1].CategoryID == _CID5);
            Assert.IsTrue(_Site[1].Priority == 1);
        }
        [TestMethod]
        public void DeleteTest()
        {
            SettingUp();
            new CategoryBLL().Delete(_CID, _CustomerUser.Id);
            var _Site = new CategoryBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsTrue(_Site.Count == 1);
            Assert.IsTrue(_Site[0].CategoryItems.Count == 1);
            Assert.IsTrue(_Site[0].Priority ==0);
            Assert.IsTrue(_Site[0].CategoryID == _CID5);
            Assert.IsTrue(_Site[0].CategoryItems[0].CategoryID == _CID7);
        }
    }
}
