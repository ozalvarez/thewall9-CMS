using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace thewall9.bll.test
{
    [TestClass]
    public class PageBLLTest : BaseTest
    {
        int _PID1;
        /*
         * Alias=Home Test
        */
        private void SettingUp()
        {
            _PID1 = new PageBLL().Save(new data.binding.PageBinding
            {
                Alias = "Home Test",
                SiteID = _SiteID,
                Published = true,
                PageParentID = 0
            });
            new PageBLL().SaveCulture(new data.binding.PageCultureBinding
            {
                CultureID = _Cultures[0].CultureID,
                FriendlyUrl = "",
                Name = "Home " + _Cultures[0].Name,
                PageID = _PID1,
                Published = true,
                TitlePage = "Home " + _Cultures[0].Name,
                SiteID=_SiteID
            }, _CustomerUser.Id);
            new PageBLL().SaveCulture(new data.binding.PageCultureBinding
            {
                CultureID = _Cultures[1].CultureID,
                FriendlyUrl = "en",
                Name = "Home " + _Cultures[1].Name,
                PageID = _PID1,
                Published = true,
                TitlePage = "Home " + _Cultures[1].Name,
                SiteID = _SiteID
            }, _CustomerUser.Id);
            Assert.IsNotNull(_PID1);

        }
        [TestMethod]
        public void PageGetPage()
        {
            SettingUp();
            var _Page = new PageBLL().GetPage(_SiteID, null, "");
            Assert.IsNotNull(_Page);
        }
        [TestMethod]
        public void PageGetPageFriendlyUrl()
        {
            SettingUp();
            var _Page = new PageBLL().GetWithCultures(_SiteID);
            Assert.IsTrue(_Page.Count > 0);
            var _FURL = new PageBLL().GetPageFriendlyUrl(_SiteID, null, null, "en");
            Assert.IsTrue(_FURL == "en");
        }
        [TestMethod]
        public void PageSaveOGraphInfo()
        {
            SettingUp();
            var _PageModel = new data.binding.PageCultureBinding
            {
                CultureID = _Cultures[1].CultureID,
                FriendlyUrl = "get-odata",
                Name = "Home " + _Cultures[1].Name,
                PageID = _PID1,
                Published = true,
                TitlePage = "Home " + _Cultures[1].Name,
                SiteID = _SiteID,
                OGraph = new data.binding.OGraphBinding
                {
                    OGraphDescription = "odata:desc",
                    OGraphTitle = "odata:title",
                    FileRead = GetImgFileRead()
                }
            };
            new PageBLL().SaveCulture(_PageModel, _CustomerUser.Id);
            var _Page = new PageBLL().GetPage(_SiteID, null, "get-odata");
            Assert.IsNotNull(_Page);
            Assert.IsTrue(_Page.Page.OGraph.OGraphDescription == "odata:desc");
            Assert.IsTrue(_Page.Page.OGraph.OGraphTitle == "odata:title");
            Assert.IsNotNull(_Page.Page.OGraph.FileRead.MediaUrl);

            _PageModel.OGraph.OGraphID = _Page.Page.OGraph.OGraphID;
            _PageModel.OGraph.FileRead.MediaID = _Page.Page.OGraph.FileRead.MediaID;
            _PageModel.OGraph.FileRead.FileContent = null;
            _PageModel.OGraph.OGraphDescription = "odata:desc2";

            new PageBLL().SaveCulture(_PageModel, _CustomerUser.Id);
            var _Page2 = new PageBLL().GetPage(_SiteID, null, "get-odata");
            Assert.IsNotNull(_Page2);
            Assert.IsTrue(_Page2.Page.OGraph.OGraphDescription == "odata:desc2");
            Assert.IsTrue(_Page2.Page.OGraph.OGraphTitle == "odata:title");
            Assert.IsNotNull(_Page2.Page.OGraph.FileRead.MediaUrl);

            Assert.IsTrue(_Page2.Page.OGraph.OGraphID == _Page.Page.OGraph.OGraphID);
            Assert.IsTrue(_Page2.Page.OGraph.FileRead.MediaID == _Page.Page.OGraph.FileRead.MediaID);
        }
    }
}
