using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Net;

namespace thewall9.bll.test
{
    [TestClass]
    public class SiteBLLTest : BaseTest
    {

        //[TestMethod]
        //public void ImportSiteTest()
        //{
        //    string _Content =System.IO.File.ReadAllText(@"../../../webs/Site.JSON");
        //    byte[] toEncodeAsBytes= System.Text.ASCIIEncoding.ASCII.GetBytes(_Content);
        //    _Content = "64base,"+System.Convert.ToBase64String(toEncodeAsBytes);
        //    new SiteBLL().Import(new data.binding.FileRead
        //    {
        //        FileContent = _Content,
        //        FileName = "Site.json"
        //    });
        //}
        [TestMethod]
        public void ExportSiteTest()
        {
            var _URL=new SiteBLL().Export(_SiteID);
            Assert.IsNotNull(_URL);
        }
        [TestMethod]
        public void ImportSiteTest()
        {
            var _URL = new SiteBLL().Export(_SiteID);
            string _Content = new WebClient().DownloadString(_URL);
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(_Content);
            _Content = "64base," + System.Convert.ToBase64String(toEncodeAsBytes);
            var _NewSiteID=new SiteBLL().Import(new data.binding.FileRead
            {
                FileContent = _Content,
                FileName = "Site.json"
            });
            Assert.IsNotNull(_NewSiteID);
            Assert.IsTrue(_NewSiteID!=0);
            var _Cultures=new CultureBLL().Get(_NewSiteID);
            Assert.IsNotNull(_Cultures);
            Assert.IsTrue(_Cultures.Count != 0);
            Assert.IsTrue(_Cultures[0].Facebook == "f" + _Cultures[0].Name);
            Assert.IsTrue(_Cultures[1].Facebook == "f" + _Cultures[1].Name);
        }
    }
}
