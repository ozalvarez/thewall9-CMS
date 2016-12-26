using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    public class BrandBase
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        public string IconUrl { get; set; }
        public int SiteID { get; set; }
        public BrandBase() { }
        public BrandBase(BrandBase Model) { SetValues(Model); }
        public void SetValues(BrandBase Model)
        {
            this.BrandName = Model.BrandName;
            this.BrandDescription = Model.BrandDescription;
            this.IconUrl = Model.IconUrl;
            this.SiteID = Model.SiteID;
        }
    }
    public class BrandBinding : BrandBase
    {
        public FileReadBinding Icon { get; set; }
    }
}
