using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace thewall9.bll.test
{
    [TestClass]
    public class PageBLLTest
    {
        [TestMethod]
        public void GetPage()
        {
            var _Page=new PageBLL().GetPage(1, null, null);
            Assert.IsNotNull(_Page);
        }
    }
}
