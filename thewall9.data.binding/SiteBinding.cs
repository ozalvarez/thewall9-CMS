using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    public enum SiteUserType
    {
        CONTENT = 1
    }
    public class SiteBase
    {
        public int SiteID { get; set; }
        public string DefaultLang { get; set; }
        public string SiteName { get; set; }
        public string GAID { get; set; }
        public bool Enabled { get; set; }
        public DateTime DateCreated { get; set; }
        public bool ECommerce { get; set; }
        public bool Blog { get; set; }
    }
    public class SiteBinding : SiteBase
    {

    }
    public class SiteFullBinding
    {
        public SiteBinding Site { get; set; }
        public List<PageCultureBinding> Menu { get; set; }
        public List<PageCultureBinding> EcommercePages { get; set; }
        public List<PageCultureBinding> OtherPages { get; set; }
        public ContentBindingList ContentLayout { get; set; }
        public List<CurrencyBinding> Currencies { get; set; }
    }
    public class SiteAllBinding : SiteBinding
    {
        public string Url { get; set; }
        public SiteAllBinding() { }
        public SiteAllBinding(SiteBinding Site, List<CultureBase> Cultures)
        {
            foreach (PropertyInfo prop in Site.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(Site, null), null);
            var _Cultures = new List<CultureBinding>();
            foreach (var item in Cultures)
            {
                _Cultures.Add(new CultureBinding
                {
                    Name = item.Name,
                    Facebook = item.Facebook,
                    GPlus = item.GPlus,
                    Instagram = item.Instagram,
                    Tumblr = item.Tumblr,
                    Twitter = item.Twitter,
                    YoutubeChannel=item.YoutubeChannel
                });
            }
            this.Cultures = _Cultures;
        }
        public IEnumerable<CultureBinding> Cultures { get; set; }
    }
    public class SiteByUserBinding : SiteBinding
    {
        public IEnumerable<SiteUserType> Roles { get; set; }
    }
    public class SiteEnabledBinding
    {
        public int SiteID { get; set; }
        public bool Enabled { get; set; }
    }
    public class AddUserInSiteBinding
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int SiteID { get; set; }
    }
    public class SiteListUsers
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<SiteUserType> Roles { get; set; }
    }
    public class SiteAddRol
    {
        public string UserID { get; set; }
        public int SiteID { get; set; }
        public bool Enabled { get; set; }
        public SiteUserType SiteUserType { get; set; }
    }

    public class SiteExport
    {
        public SiteBinding Site { get; set; }
        public List<CultureBase> Cultures { get; set; }
        public List<PageBindingListWithCultures> Pages { get; set; }
        public List<ContentBindingList> Content { get; set; }
    }

}
