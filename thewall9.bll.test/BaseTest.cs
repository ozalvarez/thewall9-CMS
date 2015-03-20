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

        //[ClassInitialize()]
        //public static void ClassInit(TestContext context)
        //{

        //}
        [TestInitialize()]
        public void Initialize()
        {
            //CREATING USERCUSTOMER
            _CustomerUser = new UserBLL().Find(EMAIL);
            if (_CustomerUser == null)
                _CustomerUser = new UserBLL().Create("Oz in Test",EMAIL, "123456");

            //CREATING A WEBSITE
            var _C = new List<CultureBinding>();
            _C.Add(new CultureBinding
            {
                Name = "es"
            });
            var _W = new data.binding.SiteAllBinding
            {
                SiteName = PREFIX + DateTime.Now.ToShortTimeString(),
                Cultures = _C
            };
            _SiteID=new SiteBLL().Save(_W);
            new SiteBLL().AddUserToAllRoles(new AddUserInSiteBinding
            {
                Email = EMAIL,
                SiteID = _SiteID
            });
        }
        [TestCleanup()]
        public void Cleanup()
        {
            //REMOVING A WEBSITE
            new SiteBLL().RemoveSites(PREFIX);
        }
        //[ClassCleanup()]
        //public static void ClassCleanup()
        //{

        //}
        public BaseTest()
        {

        }
    }
}
