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
                TitlePage = "Home " + _Cultures[0].Name
            });
            new PageBLL().SaveCulture(new data.binding.PageCultureBinding
            {
                CultureID = _Cultures[1].CultureID,
                FriendlyUrl = "en",
                Name = "Home " + _Cultures[1].Name,
                PageID = _PID1,
                Published = true,
                TitlePage = "Home " + _Cultures[1].Name
            });
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
            var _FURL=new PageBLL().GetPageFriendlyUrl(_SiteID, null, null, "en");
            Assert.IsTrue(_FURL=="en");
        }
    }
}
