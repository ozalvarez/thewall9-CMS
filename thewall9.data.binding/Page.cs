using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    public class PageBinding
    {
        public int PageID { get; set; }
        public string Alias { get; set; }
        public bool Published { get; set; }
        public int Priority { get; set; }
        public bool InMenu { get; set; }
        public int SiteID { get; set; }
        public int PageParentID { get; set; }
    }

    public class PageCultureBase
    {
        public string Name { get; set; }
        public string TitlePage { get; set; }
        public string MetaDescription { get; set; }
        public string FriendlyUrl { get; set; }
        public string ViewRender { get; set; }
        public string RedirectUrl { get; set; }
        public bool Published { get; set; }
    }
    public class PageCultureBinding : PageCultureBase
    {
        public int SiteID { get; set; }
        public int PageID { get; set; }
        public int CultureID { get; set; }
        public string CultureName { get; set; }
        public string PageAlias { get; set; }
        public List<PageCultureBinding> Items { get; set; }
    }
    public class PageWeb
    {
        public PageCultureBinding Page { get; set; }
        public ContentBindingList Content { get; set; }
    }
    public class PageBindingList : PageBinding
    {
        public ICollection<PageBindingList> Items { get; set; }
    }
    public class PageBindingListWithCultures : PageBinding
    {
        public ICollection<PageBindingListWithCultures> Items { get; set; }
        public List<PageCultureBinding> PageCultures { get; set; }
    }
    public class MoveBinding
    {
        public int Index { get; set; }
        public int PageParentID { get; set; }
        public int PageID { get; set; }
    }
    public class PublishBinding
    {
        public int PageID { get; set; }
        public bool Published { get; set; }
    }
}
