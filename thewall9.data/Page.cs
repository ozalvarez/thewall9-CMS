using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data.binding;

namespace thewall9.data
{
    public class Page : PageBinding
    {

        [ForeignKey("SiteID")]
        public virtual Site Site { get; set; }
        public virtual ICollection<PageCulture> PageCultures { get; set; }
    }
    public class PageCulture : PageCultureBase
    {
        [Key, Column(Order = 1)]
        public int PageID { get; set; }
        [Key, Column(Order = 2)]
        public int CultureID { get; set; }
        [ForeignKey("PageID")]
        public virtual Page Page { get; set; }
        [ForeignKey("CultureID")]
        public virtual Culture Culture { get; set; }
        public PageCulture() { }
        public PageCulture(PageCultureBinding Model)
        {
            SetValues(Model);
        }
        public void SetValues(PageCultureBinding Model)
        {
            this.PageID = Model.PageID;
            this.CultureID = Model.CultureID;
            this.FriendlyUrl = Model.FriendlyUrl==null?"":Model.FriendlyUrl;
            this.MetaDescription = Model.MetaDescription;
            this.Published = Model.Published;
            this.TitlePage = Model.TitlePage;
            this.ViewRender = Model.ViewRender;
            this.RedirectUrl = Model.RedirectUrl;
            this.Name = Model.Name;
        }
    }

}
