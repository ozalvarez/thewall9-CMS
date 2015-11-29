using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        ----*/
        data.binding.ContentBinding D0,D00,D01,D010,D011;
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
                ContentPropertyParentID= D0.ContentPropertyID
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

            var _Tree=new ContentBLL().GetTree(_SiteID, _Cultures[0].CultureID, _CustomerUser.Id);
            new ContentBLL().SaveTree(new data.binding.ContentTreeBinding {
                CultureID=_Cultures[0].CultureID,
                Items=_Tree,
                SiteID=_SiteID
            }, _CustomerUser.Id);
            Assert.IsNotNull(_Tree);
        }
        #region WEB
        [TestMethod]
        public void ContentGetWeb()
        {
            SettingUp();
            var _Model = new ContentBLL().GetContent(_SiteID, null, D0.ContentPropertyAlias, _Cultures[0].Name);
            Assert.IsNotNull(_Model);
        }
        #endregion

        [TestMethod]
        public void ContentSaveTest()
        {
            SettingUp();
            int _CPID1 = new ContentBLL().Save(new data.binding.ContentBinding
            {
                ContentPropertyAlias = "0",
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
            Assert.IsTrue(_Tree[1].Items.Count == 2);
        }
        #endregion
    }
}
