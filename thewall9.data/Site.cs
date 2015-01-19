using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data.binding;
using thewall9.data.Models;

namespace thewall9.data
{
    public class Site : SiteBase
    {
        public virtual List<SiteUser> SiteUsers { get; set; }
        public virtual List<Culture> Cultures { get; set; }
        public virtual List<ContentProperty> ContentProperties { get; set; }
        public virtual List<Page> Pages { get; set; }
        public virtual List<SiteUrl> SiteUrls { get; set; }
        public Site()
        {
            Cultures = new List<Culture>();
        }
    }
    public class SiteUser
    {
        [Key, Column(Order = 1)]
        public int SiteID { get; set; }
        [Key, Column(Order = 2)]
        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("SiteID")]
        public virtual Site Site { get; set; }
        public virtual List<SiteUserRol> SiteUserRoles { get; set; }
    }
    public class SiteUserRol
    {
        [Key, Column(Order = 1), ForeignKey("SiteUser")]
        public int SiteID { get; set; }
        [Key, Column(Order = 2), ForeignKey("SiteUser")]
        public string UserID { get; set; }
        [Key, Column(Order = 3)]
        public SiteUserType SiteUserType { get; set; }
        public virtual SiteUser SiteUser { get; set; }
    }
    public class SiteUrl
    {
        public int SiteUrlID { get; set; }
        public int SiteID { get; set; }
        public string Url { get; set; }
        
        [ForeignKey("SiteID")]
        public virtual Site Site { get; set; }
    }
}
