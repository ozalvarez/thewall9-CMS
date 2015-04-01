using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using thewall9.data.binding;
using System.Collections.Generic;
using thewall9.bll.Exceptions;
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
        private int _CID9;
        private List<CategoryCultureBinding> _CC;

        /*
         * TREE TEST SETTINGUP
         *  00
         *      00 - 00
         *      00 - 01
         *      00 - 02
         *      00 - 03
         *          00 - 03 - 00
         *      00 - 04
         *  01
         *      01 -00
         */
        private void SettingUp()
        {
            _CC = new List<CategoryCultureBinding>();
            _CC.Add(new CategoryCultureBinding
            {
                Adding = true,
                CategoryName = "IN ES",
                CultureID = _Cultures[0].CultureID
            });
            _CC.Add(new CategoryCultureBinding
            {
                Adding = true,
                CategoryName = "IN EN",
                CultureID = _Cultures[1].CultureID
            });

            _CID = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00",
                SiteID = _SiteID,
                CategoryCultures = _CC
            }, _CustomerUser.Id);
            _CC[0].CategoryName += DateTime.Now.ToString();
            _CC[1].CategoryName += DateTime.Now.ToString();
            _CID2 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00 - 00",
                SiteID = _SiteID,
                CategoryParentID = _CID,
                CategoryCultures = _CC
            }, _CustomerUser.Id);
            _CC[0].CategoryName += DateTime.Now.ToString();
            _CC[1].CategoryName += DateTime.Now.ToString();
            _CID3 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00 - 01",
                SiteID = _SiteID,
                CategoryParentID = _CID,
                CategoryCultures = _CC
            }, _CustomerUser.Id);
            _CC[0].CategoryName += DateTime.Now.ToString();
            _CC[1].CategoryName += DateTime.Now.ToString();
            _CID4 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00 - 02",
                SiteID = _SiteID,
                CategoryParentID = _CID,
                CategoryCultures = _CC
            }, _CustomerUser.Id);
            _CC[0].CategoryName += DateTime.Now.ToString();
            _CC[1].CategoryName += DateTime.Now.ToString();
            _CID6 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00 - 03",
                SiteID = _SiteID,
                CategoryParentID = _CID,
                CategoryCultures = _CC
            }, _CustomerUser.Id);
            _CC[0].CategoryName += DateTime.Now.ToString();
            _CC[1].CategoryName += DateTime.Now.ToString();
            _CID8 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00 - 03 - 00",
                SiteID = _SiteID,
                CategoryParentID = _CID6,
                CategoryCultures = _CC
            }, _CustomerUser.Id);
            _CC[0].CategoryName += DateTime.Now.ToString();
            _CC[1].CategoryName += DateTime.Now.ToString();
            _CID9 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "00 - 04",
                SiteID = _SiteID,
                CategoryParentID = _CID,
                CategoryCultures = _CC
            }, _CustomerUser.Id);
            _CC[0].CategoryName += DateTime.Now.ToString();
            _CC[1].CategoryName += DateTime.Now.ToString();
            _CID5 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "01",
                SiteID = _SiteID,
                CategoryCultures = _CC
            }, _CustomerUser.Id);
            _CC[0].CategoryName += DateTime.Now.ToString();
            _CC[1].CategoryName += DateTime.Now.ToString();
            _CID7 = new CategoryBLL().Save(new data.binding.CategoryBinding
            {
                CategoryAlias = "01 - 00",
                SiteID = _SiteID,
                CategoryParentID = _CID5,
                CategoryCultures = _CC
            }, _CustomerUser.Id);
            Assert.IsNotNull(_CID);
        }
        [TestMethod]
        public void CategoryUpdateTest()
        {
            SettingUp();
            _CID4 = new CategoryBLL().Save(new data.binding.CategoryBinding
           {
               CategoryID = _CID4,
               CategoryAlias = "02",
               SiteID = _SiteID,
               CategoryParentID = 0
           }, _CustomerUser.Id);
            var _Site = new CategoryBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsTrue(_Site.Count == 3);
            Assert.IsTrue(_Site[2].Priority == 2);
            Assert.IsTrue(_Site[0].CategoryItems[2].CategoryID == _CID6);
            Assert.IsTrue(_Site[0].CategoryItems[2].Priority == 2);
            Assert.IsTrue(_Site[0].CategoryItems[3].CategoryID == _CID9);
            Assert.IsTrue(_Site[0].CategoryItems[3].Priority == 3);

        }
        [TestMethod]
        public void CategorySaveTest()
        {
            SettingUp();
        }
        [TestMethod]
        public void CategoryGetTreeTest()
        {
            SettingUp();
            var _Site = new CategoryBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsTrue(_Site.Count == 2);
            Assert.IsTrue(_Site[0].CategoryItems.Count == 5);
            Assert.IsTrue(_Site[1].Priority == 1);
            Assert.IsTrue(_Site[0].CategoryItems[2].Priority == 2);
            Assert.IsTrue(_Site[0].CategoryItems[3].CategoryItems.Count == 1);
        }
        [TestMethod]
        public void CategoryUpTest()
        {
            SettingUp();
            new CategoryBLL().UpOrDown(new UpOrDown
            {
                CategoryID = _CID4,
                Up = true
            }, _CustomerUser.Id);
            var _Site = new CategoryBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsTrue(_Site[0].CategoryItems[1].CategoryID == _CID4);
            Assert.IsTrue(_Site[0].CategoryItems[1].Priority == 1);
            Assert.IsTrue(_Site[0].CategoryItems[2].CategoryID == _CID3);
            Assert.IsTrue(_Site[0].CategoryItems[2].Priority == 2);
        }
        [TestMethod]
        public void CategoryUpFirstItemTest()
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
        [TestMethod]
        public void CategoryDownTest()
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
        public void CategoryDownLastItemTest()
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
        public void CategoryDeleteTest()
        {
            SettingUp();
            new CategoryBLL().Delete(_CID, _CustomerUser.Id);
            var _Site = new CategoryBLL().Get(_SiteID, _CustomerUser.Id);
            Assert.IsTrue(_Site.Count == 1);
            Assert.IsTrue(_Site[0].CategoryItems.Count == 1);
            Assert.IsTrue(_Site[0].Priority == 0);
            Assert.IsTrue(_Site[0].CategoryID == _CID5);
            Assert.IsTrue(_Site[0].CategoryItems[0].CategoryID == _CID7);
        }
        [TestMethod]
        public void CategoryGetWebTreeTest()
        {
            SettingUp();
            var _Categories = new CategoryBLL().Get(_SiteID, null, 0, _Cultures[0].Name, null);
            Assert.IsTrue(_Categories.Count == 2);
            Assert.IsTrue(_Categories[0].CategoryItems.Count == 5);
            Assert.IsTrue(_Categories[0].CategoryItems[3].CategoryItems.Count == 1);
        }
        [TestMethod]
        public void CategoryGetWebWithParentTreeTest()
        {
            SettingUp();
            var _Categories = new CategoryBLL().Get(_SiteID, null, _CID, _Cultures[0].Name, null);
            Assert.IsTrue(_Categories.Count == 5);
            Assert.IsTrue(_Categories[3].CategoryItems.Count == 1);
        }
        [TestMethod]
        public void CategoryValidateDuplicateFriendlyUrlTest()
        {
            SettingUp();
            var _FUrl = Utils.Util.RandomString(10) + DateTime.Now.ToString();
            _CC[0].FriendlyUrl = _FUrl;
            _CC[1].FriendlyUrl = _FUrl;
            try
            {
                new CategoryBLL().Save(new data.binding.CategoryBinding
                     {
                         CategoryAlias = "02",
                         SiteID = _SiteID,
                         CategoryParentID = 0,
                         CategoryCultures = _CC
                     }, _CustomerUser.Id);
                Assert.Fail();
            }
            catch (RuleException e)
            {
                Assert.IsTrue(e.CodeRuleException.Equals("0x002"));
            }
        }
        [TestMethod]
        public void CategoryValidateDuplicateNameTest()
        {
            SettingUp();
            var _FUrl = Utils.Util.RandomString(10) + DateTime.Now.ToString();
            _CC[0].CultureName = _FUrl;
            _CC[1].CultureName = _FUrl;
            _CC[0].FriendlyUrl = null;
            _CC[1].FriendlyUrl = null;
            try
            {
                new CategoryBLL().Save(new data.binding.CategoryBinding
                {
                    CategoryAlias = "02",
                    SiteID = _SiteID,
                    CategoryParentID = 0,
                    CategoryCultures = _CC
                }, _CustomerUser.Id);
                Assert.Fail();
            }
            catch (RuleException e)
            {
                Assert.IsTrue(e.CodeRuleException.Equals("0x001"));
            }
        }
    }
}
