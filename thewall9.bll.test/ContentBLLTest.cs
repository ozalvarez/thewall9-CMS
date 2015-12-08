using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using thewall9.data.Models;
using System.Collections.Generic;
using thewall9.data.binding;

namespace thewall9.bll.test
{
    [TestClass]
    public class ContentBLLTest : BaseTest
    {
        /*--- SETTING UP TREE
        0
            0-0 [TXT]
            0-1
                0-1-0 [TXT]
                0-1-1 [TXT]
                0-1-2 [LIST]
                    0-1-2-0 [TXT]
        ----*/
        data.binding.ContentBinding D0, D00, D01, D010, D011, D012, D0120;

        private void SettingUp()
        {
            D0 = new data.binding.ContentBinding
            {
                ContentPropertyAlias = "0",
                ContentPropertyType = data.binding.ContentPropertyType.LIST,
                SiteID = _SiteID
            };
            D0.ContentPropertyID = new ContentBLL().Save(D0);

            D00 = new data.binding.ContentBinding
            {
                ContentPropertyAlias = "0-0",
                ContentPropertyType = data.binding.ContentPropertyType.TXT,
                SiteID = _SiteID,
                ContentPropertyParentID = D0.ContentPropertyID
            };
            D00.ContentPropertyID = new ContentBLL().Save(D00);

            D01 = new data.binding.ContentBinding
            {
                ContentPropertyAlias = "0-1",
                ContentPropertyType = data.binding.ContentPropertyType.LIST,
                SiteID = _SiteID,
                ContentPropertyParentID = D0.ContentPropertyID
            };
            D01.ContentPropertyID = new ContentBLL().Save(D01);

            D010 = new data.binding.ContentBinding
            {
                ContentPropertyAlias = "0-1-0",
                ContentPropertyType = data.binding.ContentPropertyType.TXT,
                SiteID = _SiteID,
                ContentPropertyParentID = D01.ContentPropertyID
            };
            D010.ContentPropertyID = new ContentBLL().Save(D010);

            D011 = new data.binding.ContentBinding
            {
                ContentPropertyAlias = "0-1-1",
                ContentPropertyType = data.binding.ContentPropertyType.TXT,
                SiteID = _SiteID,
                ContentPropertyParentID = D01.ContentPropertyID
            };
            D011.ContentPropertyID = new ContentBLL().Save(D011);

            D012 = new data.binding.ContentBinding
            {
                ContentPropertyAlias = "0-1-2",
                ContentPropertyType = data.binding.ContentPropertyType.LIST,
                SiteID = _SiteID,
                ContentPropertyParentID = D01.ContentPropertyID
            };
            D012.ContentPropertyID = new ContentBLL().Save(D012);

            D0120 = new data.binding.ContentBinding
            {
                ContentPropertyAlias = "0-1-2-0",
                ContentPropertyType = data.binding.ContentPropertyType.TXT,
                SiteID = _SiteID,
                ContentPropertyParentID = D012.ContentPropertyID
            };
            D0120.ContentPropertyID = new ContentBLL().Save(D0120);

            var _Tree = new ContentBLL().GetTree(_SiteID, _Cultures[0].CultureID, _CustomerUser.Id);
            new ContentBLL().SaveTree(new data.binding.ContentTreeBinding
            {
                CultureID = _Cultures[0].CultureID,
                Items = _Tree,
                SiteID = _SiteID
            }, _CustomerUser.Id);
            Assert.IsNotNull(_Tree);
        }
        private void TreeComplete(data.binding.ContentBindingList Model)
        {
            Assert.IsNotNull(Model);
            Assert.IsTrue(Model.Items.Count == 2);
            Assert.IsTrue((Model.Items as List<data.binding.ContentBindingList>)[1].Items.Count == 3);
        }

        #region UTILS
        [TestMethod]
        public void ContentGetParentsID()
        {
            SettingUp();
            List<int> _List = new List<int>();
            new ContentBLL().GetParentsID(ref _List, D0120.ContentPropertyID);
            Assert.IsTrue(_List.Count == 4);
            Assert.IsTrue(_List[0] == D012.ContentPropertyID);
            Assert.IsTrue(_List[1] == D01.ContentPropertyID);
            Assert.IsTrue(_List[2] == D0.ContentPropertyID);
        }
        [TestMethod]
        public void ContentGetChildsID()
        {
            SettingUp();
            List<int> _List = new List<int>();
            new ContentBLL().GetChildsID(ref _List, D0.ContentPropertyID);
            Assert.IsTrue(_List.Count == 6);
        }
        #endregion

        #region ROOTS
        [TestMethod]
        public void ContentRootAdd()
        {
            SettingUp();
            var _Model = new data.binding.ContentBinding
            {
                ContentPropertyAlias = "0-1-2-1",
                ContentPropertyType = data.binding.ContentPropertyType.LIST,
                SiteID = _SiteID,
                ContentPropertyParentID = D012.ContentPropertyID
            };
            var _ID = new ContentBLL().Save(_Model);
            var _List = new ContentBLL().GetRootParentsID(_ID);
            Assert.IsTrue(_List.Count == 4);

            //UPDATING
            _Model.ContentPropertyID = _ID;
            new ContentBLL().Save(_Model);
            _List = new ContentBLL().GetRootParentsID(_ID);
            Assert.IsTrue(_List.Count == 4);
        }
        [TestMethod]
        public void ContentRootDelete()
        {
            SettingUp();
            new ContentBLL().Delete(D01.ContentPropertyID,_CustomerUser.Id);
            var _List = new ContentBLL().GetRootChilds(D0.ContentPropertyID);
            Assert.IsNotNull(_List);
            Assert.IsTrue(_List.Count == 1);
            Assert.IsTrue(_List[0] == D00.ContentPropertyID);
        }
        [TestMethod]
        public void ContentRootMove()
        {
            SettingUp();
            var _Move = new ContentMoveBinding
            {
                ContentPropertyID = D012.ContentPropertyID,
                ContentPropertyParentID = 0,
                Index = 1
            };
            new ContentBLL().Move(_Move, _CustomerUser.Id);
            var _List = new ContentBLL().GetRootChilds(D012.ContentPropertyID);
            Assert.IsTrue(_List.Count == 1);
            _List = new ContentBLL().GetRootParentsID(D012.ContentPropertyID);
            Assert.IsTrue(_List.Count == 1);
            _List = new ContentBLL().GetRootParentsID(D0120.ContentPropertyID);
            Assert.IsTrue(_List.Count == 2);
        }
        #endregion

        #region WEB
        [TestMethod]
        public void ContentGetWeb()
        {
            using (var _c = new ApplicationDbContext())
            {
                SettingUp();
                var _Logger = new MyLogger();
                _c.Database.Log = s => _Logger.Log("EFApp", s);
                var _Model = new ContentBLL().GetContent(_SiteID, null, D0.ContentPropertyAlias, _Cultures[0].Name, _c);
                TreeComplete(_Model);
                Assert.IsNotNull(_Model);
             //   Assert.IsTrue(_Logger.NumberSQLQuery < 14);
            }
        }
        [TestMethod]
        public void ContentGetByRoot()
        {
            using (var _c = new ApplicationDbContext())
            {
                SettingUp();
                var _Logger = new MyLogger();
                _c.Database.Log = s => _Logger.Log("EFApp", s);
                //WORKANDGO
                //var _Model = new ContentBLL().GetContent2(2, null, "place-list", "es", _c);
                TreeComplete(new ContentBLL().GetByRoot(_SiteID, null, D0.ContentPropertyAlias, _Cultures[0].Name, _c));
                Assert.IsTrue(_Logger.NumberSQLQuery == 2);
            }
        }
        #endregion

        [TestMethod]
        public void ContentSaveTest()
        {
            SettingUp();
            int _CPID1 = new ContentBLL().Save(new data.binding.ContentBinding
            {
                ContentPropertyAlias = "1",
                ContentPropertyType = data.binding.ContentPropertyType.LIST,
                SiteID = _SiteID
            });
            Assert.IsNotNull(_CPID1);
            Assert.IsTrue(_CPID1 != 0);
        }

        #region Menu
        [TestMethod]
        public void ContentGetMenuTest()
        {
            SettingUp();
            new ContentBLL().InMenu(new data.binding.ContentBoolean
            {
                Boolean = true,
                ContentPropertyID = D01.ContentPropertyID,
            }, _CustomerUser.Id);
            var _Menu = new ContentBLL().GetMenu(_SiteID, _Cultures[0].CultureID, _CustomerUser.Id);
            Assert.IsNotNull(_Menu);
            Assert.IsTrue(_Menu[1].ContentPropertyID == D01.ContentPropertyID);
        }
        [TestMethod]
        public void ContentGetTreeByPropertyTest()
        {
            SettingUp();
            var _Tree = new ContentBLL().GetTreeByContentProperty(D0.ContentPropertyID, _Cultures[0].CultureID, _CustomerUser.Id);
            Assert.IsNotNull(_Tree);
            Assert.IsTrue(_Tree[0].ContentPropertyID == D00.ContentPropertyID);
            Assert.IsTrue(_Tree[1].ContentPropertyID == D01.ContentPropertyID);
            Assert.IsTrue(_Tree[1].Items.Count == 3);
        }
        #endregion

        [TestMethod]
        public void ContentMigrator()
        {
            new ContentBLL().Migrator();
        }

    }
}
