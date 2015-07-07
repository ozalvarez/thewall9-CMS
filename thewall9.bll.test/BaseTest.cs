using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using thewall9.data.Models;
using thewall9.data.binding;
using System.Collections.Generic;

namespace thewall9.bll.test
{
    [TestClass]
    public class BaseTest
    {
        private const string PREFIX = "test-";
        public String EMAIL = "test@thewall9.com";

        protected int _SiteID;
        protected ApplicationUser _CustomerUser;
        protected List<CultureBase> _Cultures;

        [TestInitialize()]
        public void Initialize()
        {
            //CREATING USERCUSTOMER
            _CustomerUser = new UserBLL().Find(EMAIL);
            if (_CustomerUser == null)
                _CustomerUser = new UserBLL().Create("Oz in Test", EMAIL, "123456");

            //CREATING A WEBSITE
            var _C = new List<CultureBinding>();
            _C.Add(new CultureBinding
            {
                Name = "es"
            });
            _C.Add(new CultureBinding
            {
                Name = "en"
            });
            var _W = new data.binding.SiteAllBinding
            {
                SiteName = PREFIX + DateTime.Now.ToShortTimeString(),
                Cultures = _C,
                DefaultLang="es"
            };
            _SiteID = new SiteBLL().Save(_W);
            new SiteBLL().AddUserToAllRoles(new AddUserInSiteBinding
            {
                Email = EMAIL,
                SiteID = _SiteID
            });

            _Cultures = new CultureBLL().Get(_SiteID);
            new CultureBLL().Save(new CultureBinding
            {
                CultureID = _Cultures[0].CultureID,
                SiteID = _SiteID,
                Facebook = "f" + _Cultures[0].Name,
                GPlus = "g" + _Cultures[0].Name,
                Instagram = "i" + _Cultures[0].Name,
                Tumblr = "t" + _Cultures[0].Name
            }, _CustomerUser.Id);
            new CultureBLL().Save(new CultureBinding
            {
                CultureID = _Cultures[1].CultureID,
                Facebook = "f" + _Cultures[1].Name,
                GPlus = "g" + _Cultures[1].Name,
                Instagram = "i" + _Cultures[1].Name,
                Tumblr = "t" + _Cultures[1].Name,
                SiteID = _SiteID,
            }, _CustomerUser.Id);
        }
        [TestCleanup()]
        public void Cleanup()
        {
            //REMOVING A WEBSITE
            new SiteBLL().RemoveSites(PREFIX);
        }
        public BaseTest()
        {

        }

        public FileRead GetImgFileRead()
        {
            return new FileRead
            {
                FileContent = "64base," + System.Convert.ToBase64String(System.IO.File.ReadAllBytes("logo.png")),
                FileName = "logo.png"
            };
        }
    }
}
