using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.bll.Exceptions;
using thewall9.data;
using thewall9.data.binding;
using thewall9.data.Models;

namespace thewall9.bll
{
    public class PageBLL : BaseBLL
    {

        #region WEB
        string[] _EcommercePageAlias = new string[5] { "cart", "checkout", "catalog", "order-sent","product" };
        public PageWeb GetPageByAlias(int SiteID, string Url, string Alias, string Lang)
        {
            var _Page = new PageWeb();
            _Page.Page = GetPageCultureBindingByAlias(SiteID, Url, Alias,Lang);
            _Page.Content = new ContentBLL().GetContent(SiteID, Url, Alias, Lang);
            return _Page;
        }
        private PageCultureBinding GetPageCultureBindingByAlias(int SiteID, string Url, string Alias, string Lang)
        {
            using (var _c = db)
            {
                var _Q = SiteID != 0
                    ? from p in _c.PageCultures
                      where p.Page.SiteID == SiteID
                      select p
                     : from m in _c.PageCultures
                       join u in _c.SiteUrls on m.Page.Site.SiteID equals u.SiteID
                       where u.Url.Equals(Url)
                       select m;

                var _Pages = (from p in _Q
                              where p.Page.Alias.ToLower().Equals(Alias) && p.Culture.Name.ToLower().Equals(Lang)
                              select new PageCultureBinding
                              {
                                  CultureID = p.CultureID,
                                  FriendlyUrl = p.FriendlyUrl,
                                  MetaDescription = p.MetaDescription,
                                  PageID = p.PageID,
                                  Published = p.Published,
                                  TitlePage = p.TitlePage,
                                  ViewRender = p.ViewRender,
                                  RedirectUrl = p.RedirectUrl,
                                  Name = p.Name,
                                  CultureName = p.Culture.Name,
                                  PageAlias = p.Page.Alias
                              }).FirstOrDefault();
                if (_Pages == null)
                    throw new RuleException("No existe página con ese Alias en ese Lang no existe", "0x000");
                return _Pages;
            }
        }
        public PageWeb GetPage(int SiteID, string Url, string FriendlyUrl)
        {
            var _Page = new PageWeb();
            _Page.Page = GetPageCultureBinding(SiteID, Url, FriendlyUrl);
            _Page.Content = new ContentBLL().GetContent(SiteID, Url, _Page.Page.PageAlias, _Page.Page.CultureName);
            return _Page;
        }

        private PageCultureBinding GetPageCultureBinding(int SiteID, string Url, string FriendlyUrl)
        {
            using (var _c = db)
            {
                var _Q = SiteID != 0
                    ? from p in _c.PageCultures
                      where p.Page.SiteID == SiteID
                      select p
                     : from m in _c.PageCultures
                       join u in _c.SiteUrls on m.Page.Site.SiteID equals u.SiteID
                       where u.Url.Equals(Url)
                       select m;

                var _Pages = (from p in _Q
                              where FriendlyUrl == null ? p.FriendlyUrl == "" : (p.FriendlyUrl.ToLower().Equals(FriendlyUrl.ToLower()))
                              select new PageCultureBinding
                              {
                                  CultureID = p.CultureID,
                                  FriendlyUrl = p.FriendlyUrl,
                                  MetaDescription = p.MetaDescription,
                                  PageID = p.PageID,
                                  Published = p.Published,
                                  TitlePage = p.TitlePage,
                                  ViewRender = p.ViewRender,
                                  RedirectUrl = p.RedirectUrl,
                                  Name = p.Name,
                                  CultureName = p.Culture.Name,
                                  PageAlias = p.Page.Alias
                              }).ToList();
                if (_Pages == null || _Pages.Count == 0)
                    throw new RuleException("No existe página con ese FriendlyUrl", "0x000");
                return _Pages[0];
            }
        }
        public List<PageCultureBinding> GetMenu(int SiteID, string Url, string DefaultLang)
        {
            using (var _c = db)
            {
                var _Q = SiteID != 0
                    ? from p in _c.PageCultures
                      where p.Page.SiteID == SiteID
                      select p
                     : from m in _c.PageCultures
                       join u in _c.SiteUrls on m.Page.Site.SiteID equals u.SiteID
                       where u.Url.Equals(Url)
                       select m;
                return (from p in _Q
                        where p.Page.InMenu && p.Culture.Name.Equals(DefaultLang)
                        && p.Published && p.Page.Published
                        orderby p.Page.Priority
                        select new PageCultureBinding
                        {
                            CultureID = p.CultureID,
                            FriendlyUrl = p.FriendlyUrl,
                            RedirectUrl = p.RedirectUrl,
                            Name = p.Name,
                            PageAlias = p.Page.Alias
                        }).ToList();
            }
        }
        public List<PageCultureBinding> GetEcommercePages(int SiteID, string Url, string Lang)
        {
            using (var _c = db)
            {
                var _Q = SiteID != 0
                    ? from p in _c.PageCultures
                      where p.Page.SiteID == SiteID
                      select p
                     : from m in _c.PageCultures
                       join u in _c.SiteUrls on m.Page.Site.SiteID equals u.SiteID
                       where u.Url.Equals(Url)
                       select m;
                return (from p in _Q
                        where p.Culture.Name.Equals(Lang) && _EcommercePageAlias.Contains(p.Page.Alias)
                        orderby p.Page.Priority
                        select new PageCultureBinding
                        {
                            CultureID = p.CultureID,
                            FriendlyUrl = p.FriendlyUrl,
                            RedirectUrl = p.RedirectUrl,
                            Name = p.Name,
                            PageAlias = p.Page.Alias
                        }).ToList();
            }
        }
        public List<PageCultureBinding> GetOtherPages(int SiteID, string Url, string Lang)
        {
            using (var _c = db)
            {
                var _Q = SiteID != 0
                    ? from p in _c.PageCultures
                      where p.Page.SiteID == SiteID && !p.Page.InMenu
                      select p
                     : from m in _c.PageCultures
                       join u in _c.SiteUrls on m.Page.Site.SiteID equals u.SiteID
                       where u.Url.Equals(Url) && !m.Page.InMenu
                       select m;
                return (from p in _Q
                        where p.Culture.Name.Equals(Lang) && !_EcommercePageAlias.Contains(p.Page.Alias)
                        orderby p.Page.Priority
                        select new PageCultureBinding
                        {
                            CultureID = p.CultureID,
                            FriendlyUrl = p.FriendlyUrl,
                            RedirectUrl = p.RedirectUrl,
                            Name = p.Name,
                            PageAlias = p.Page.Alias
                        }).ToList();
            }
        }
        public SiteMapModel GetSitemap(int SiteID, string Url)
        {
            using (var _c = db)
            {
                bool _ECommerce = false;
                bool _Blog = false;
                if (SiteID == 0)
                {
                    var _Site=new SiteBLL().Get(Url, _c);
                    SiteID = _Site.SiteID;
                    _ECommerce = _Site.ECommerce;
                    _Blog = _Site.Blog;
                }
                else
                {
                    var _Site = new SiteBLL().Get(SiteID);
                    _ECommerce = _Site.ECommerce;
                    _Blog = _Site.Blog;
                }
                var _S = new SiteMapModel();
                _S.Pages = (from p in _c.PageCultures
                            where p.Page.SiteID == SiteID
                        where p.Published && p.Page.Published && string.IsNullOrEmpty(p.RedirectUrl)
                        select new PageCultureBinding
                        {
                            CultureID = p.CultureID,
                            FriendlyUrl = p.FriendlyUrl,
                            RedirectUrl = p.RedirectUrl,
                            Name = p.Name,
                            PageAlias = p.Page.Alias
                        }).Distinct().ToList();
                _S.Ecommerce = _ECommerce;
                _S.Blog = _Blog;
                if (_ECommerce)
                {
                    _S.Products = new ProductBLL().GetSitemap(SiteID);
                    _S.Categories = new CategoryBLL().GetSitemap(SiteID);
                }
                if (_Blog)
                {
                    _S.Posts = new BlogBLL().GetSitemap(SiteID);
                    _S.BlogCategories = new BlogBLL().GetSitemapCategories(SiteID);
                    _S.BlogTags= new BlogBLL().GetSitemapTags(SiteID);
                }
                return _S;
            }
        }
        public string GetPageFriendlyUrl(int SiteID, string Url, string FriendlyUrl, string TargetLang)
        {
            FriendlyUrl = !string.IsNullOrEmpty(FriendlyUrl) ? FriendlyUrl.Replace("/", "") : "";
            using (var _c = db)
            {
                var _Q = SiteID != 0
                    ? from p in _c.PageCultures
                      where p.Page.SiteID == SiteID
                      select p
                     : from m in _c.PageCultures
                       join u in _c.SiteUrls on m.Page.Site.SiteID equals u.SiteID
                       where u.Url.Equals(Url)
                       select m;
                var _PageID = (from p in _Q
                               where FriendlyUrl == null ? p.FriendlyUrl == "" : (p.FriendlyUrl.ToLower().Equals(FriendlyUrl.ToLower()))
                               select p.PageID).FirstOrDefault();
                var _Page = _Q.Where(m => m.PageID == _PageID && m.Culture.Name.Equals(TargetLang.ToLower())).FirstOrDefault();
                if (_Page == null)
                    throw new RuleException("No existe página con ese FriendlyUrl", "0x000");
                return _Page.FriendlyUrl;
            }
        }
        #endregion

        public PageCultureBinding Get(int PageID, int CultureID, string UserID)
        {
            using (var _c = db)
            {
                var _P = _c.Pages.Where(m => m.PageID == PageID).SingleOrDefault();
                Can(_P.SiteID, UserID, _c);
                var _Model = (from p in _c.PageCultures
                              where p.PageID == PageID && p.CultureID == CultureID
                              select new PageCultureBinding
                              {
                                  CultureID = p.CultureID,
                                  FriendlyUrl = p.FriendlyUrl,
                                  MetaDescription = p.MetaDescription,
                                  PageID = p.PageID,
                                  Published = p.Published,
                                  TitlePage = p.TitlePage,
                                  ViewRender = p.ViewRender,
                                  RedirectUrl = p.RedirectUrl,
                                  Name = p.Name,
                                  PageAlias = p.Page.Alias
                              }).SingleOrDefault();
                if (_Model == null)
                {
                    _Model = (from p in _c.Pages
                              where p.PageID == PageID
                              select new PageCultureBinding
                              {
                                  CultureID = CultureID,
                                  FriendlyUrl = null,
                                  PageID = p.PageID,
                                  Published = p.Published,
                                  TitlePage = p.Alias + " - " + p.Site.DefaultLang,
                                  ViewRender = "Index",
                                  Name = p.Alias,
                                  PageAlias = p.Alias
                              }).SingleOrDefault();
                }
                return _Model;
            }
        }
        public List<PageBindingList> Get(int SiteID, string UserID)
        {
            using (var _c = db)
            {
                Can(SiteID, UserID, _c);
                return Get(SiteID);
            }
        }
        public List<PageBindingList> Get(int SiteID)
        {
            List<Page> _List = null;
            using (var _c = db)
            {
                _List = (from p in _c.Pages
                         where p.SiteID == SiteID
                         select p).ToList();
            }
            return Order(_List, 0);
        }
        public List<PageBindingListWithCultures> GetWithCultures(int SiteID)
        {
            List<Page> _List;
            using (var _c = db)
            {
                _List = (from p in _c.Pages.Include("PageCultures.Culture")
                         where p.SiteID == SiteID
                         select p).ToList();
                return OrderWithCultures(_List, 0);
            }

        }
        private List<PageBindingListWithCultures> OrderWithCultures(List<Page> Pages, int PageParentID)
        {
            return (from p in Pages
                    where p.PageParentID == PageParentID
                    select new PageBindingListWithCultures
                    {
                        Alias = p.Alias,
                        InMenu = p.InMenu,
                        PageID = p.PageID,
                        PageParentID = p.PageParentID,
                        Priority = p.Priority,
                        Published = p.Published,
                        SiteID = p.SiteID,
                        Items = Pages.Where(m => m.PageParentID == p.PageID).Any() ? OrderWithCultures(Pages, p.PageID) : new List<PageBindingListWithCultures>(),
                        PageCultures = p.PageCultures.Select(m => new PageCultureBinding
                        {
                            CultureID = m.CultureID,
                            CultureName = m.Culture.Name,
                            FriendlyUrl = m.FriendlyUrl,
                            MetaDescription = m.MetaDescription,
                            Name = m.Name,
                            Published = m.Published,
                            RedirectUrl = m.RedirectUrl,
                            TitlePage = m.TitlePage,
                            ViewRender = m.ViewRender
                        }).ToList()
                    }).OrderBy(m => m.Priority).ToList();
        }

        public int Save(PageBinding Model, string UserID)
        {
            using (var _c = db)
            {
                Can(Model.SiteID, UserID, _c);
                return Save(Model);
            }
        }
        public int Save(PageBinding Model)
        {
            using (var _c = db)
            {
                var _IQParent = _c.Pages.Where(m => m.SiteID == Model.SiteID && m.PageParentID == Model.PageParentID);
                var _Model = new Page();
                _Model.PageParentID = Model.PageParentID;
                _Model.Alias = Model.Alias;
                _Model.SiteID = Model.SiteID;
                _Model.Priority = _IQParent.Any() ? _IQParent.Select(m => m.Priority).Max() + 1 : 0;
                _Model.InMenu = Model.InMenu;
                _Model.Published = Model.Published;
                _c.Pages.Add(_Model);
                _c.SaveChanges();
                return _Model.PageID;
            }
        }
        public int Save(PageBindingListWithCultures Model)
        {
            var _PageID = Save((PageBinding)Model);
            using (var _c = db)
            {
                var _Page = _c.Pages.Where(m => m.PageID == _PageID).SingleOrDefault();
                foreach (var item in Model.PageCultures)
                {
                    _Page.PageCultures.Add(new PageCulture
                    {
                        CultureID = _c.Cultures.Where(m => m.Name.Equals(item.CultureName) && m.SiteID == Model.SiteID).SingleOrDefault().CultureID,
                        FriendlyUrl = item.FriendlyUrl,
                        MetaDescription = item.MetaDescription,
                        Name = item.Name,
                        Published = item.Published,
                        RedirectUrl = item.RedirectUrl,
                        TitlePage = item.TitlePage,
                        ViewRender = item.ViewRender
                    });
                }
                _c.SaveChanges();
                return _PageID;
            }
        }
        public void SaveCulture(PageCultureBinding Model, string UserID)
        {
            using (var _c = db)
            {
                Can(Model.SiteID, UserID, _c);
                SaveCulture(Model);
            }
        }
        public void SaveCulture(PageCultureBinding Model)
        {
            using (var _c = db)
            {
                var _PageCulture = _c.PageCultures.Where(m => m.PageID == Model.PageID && m.CultureID == Model.CultureID).SingleOrDefault();
                if (_PageCulture == null)
                {
                    _PageCulture = new PageCulture(Model);
                    _c.PageCultures.Add(_PageCulture);
                    var _Page = _c.Pages.Where(m => m.PageID == Model.PageID).SingleOrDefault();
                    _Page.Alias = Model.PageAlias;
                }
                else
                {
                    _PageCulture.SetValues(Model);
                    _PageCulture.Page.Alias = Model.PageAlias;
                }
                //VERIFY FriendlyUrl is Unique
                if (_c.PageCultures.Where(m => (Model.FriendlyUrl != null
                    ? m.FriendlyUrl.ToLower().Equals(Model.FriendlyUrl.ToLower()) : m.FriendlyUrl.Equals(""))
                    && m.PageID != Model.PageID && m.Page.SiteID == Model.SiteID).Any())
                    throw new RuleException("Ya existe una seccion con el FriendlyUrl /" + Model.FriendlyUrl);
                _c.SaveChanges();
            }
        }
        public void Delete(int PageID, string UserID)
        {
            using (var _c = db)
            {
                var _P = _c.Pages.Where(m => m.PageID == PageID).SingleOrDefault();
                Can(_P.SiteID, UserID, _c);
                var _ItemToDelete = (from p in _c.Pages
                                     where p.PageID == PageID
                                     select p).SingleOrDefault();

                var _ItemChilds = (from p in _c.Pages
                                   where p.PageParentID == PageID
                                   select p).ToList();

                foreach (var item in _ItemChilds)
                {
                    Delete(item.PageID, UserID);
                }
                //UPDATE PRIORITY
                _c.Database.ExecuteSqlCommand(@"UPDATE Pages SET Priority=Priority-1 WHERE PageParentID={0} AND Priority>={1}", _ItemToDelete.PageParentID, _ItemToDelete.Priority);
                _c.Pages.Remove(_ItemToDelete);
                _c.SaveChanges();

            }
        }
        private List<PageBindingList> Order(List<Page> Pages, int PageParentID)
        {
            return (from p in Pages
                    where p.PageParentID == PageParentID
                    select new PageBindingList
                    {
                        Alias = p.Alias,
                        InMenu = p.InMenu,
                        PageID = p.PageID,
                        PageParentID = p.PageParentID,
                        Priority = p.Priority,
                        Published = p.Published,
                        SiteID = p.SiteID,
                        Items = Pages.Where(m => m.PageParentID == p.PageID).Any() ? Order(Pages, p.PageID) : new List<PageBindingList>()
                    }).OrderBy(m => m.Priority).ToList();
        }

        public void Move(MoveBinding Model, string UserID)
        {
            using (var _c = db)
            {
                var _P = _c.Pages.Where(m => m.PageID == Model.PageID).SingleOrDefault();
                Can(_P.SiteID, UserID, _c);
                var _Item = (from p in _c.Pages
                             where p.PageID == Model.PageID
                             select p).SingleOrDefault();
                if (Model.PageParentID == _Item.PageParentID)
                {
                    _c.Database.ExecuteSqlCommand(@"UPDATE Pages SET Priority=Priority+1 
WHERE SiteID={0} AND PageParentID={1} AND Priority>={2} AND Priority < {3} AND  PageID<>{4}", _Item.SiteID, Model.PageParentID, Model.Index, _Item.Priority, Model.PageID);

                    _c.Database.ExecuteSqlCommand(@"UPDATE Pages SET Priority=Priority-1 
WHERE SiteID={0} AND PageParentID={1} AND Priority<= {2} AND Priority >{3} AND PageID<>{4}", _Item.SiteID, _Item.PageParentID, Model.Index, _Item.Priority, Model.PageID);
                }
                else
                {
                    //SUBSTRACT OLD PARENT ID
                    _c.Database.ExecuteSqlCommand(@"UPDATE Pages SET Priority=Priority-1 
WHERE SiteID={0} AND PageParentID={1} AND Priority>= {2}  AND PageID<>{3}", _Item.SiteID, _Item.PageParentID, _Item.Priority, Model.PageID);
                    //SUM NEW PARENT ID
                    _c.Database.ExecuteSqlCommand(@"UPDATE Pages SET Priority=Priority+1 
WHERE SiteID={0} AND PageParentID={1} AND Priority>={2} AND PageID<>{3}", _Item.SiteID, Model.PageParentID, Model.Index, Model.PageID);

                }

                _Item.PageParentID = Model.PageParentID;
                _Item.Priority = Model.Index;
                _c.SaveChanges();

            }
        }
        public void Publish(PublishBinding Model, string UserID)
        {
            using (var _c = db)
            {
                var _P = _c.Pages.Where(m => m.PageID == Model.PageID).SingleOrDefault();
                Can(_P.SiteID, UserID, _c);
                var _Page = _c.Pages.Where(m => m.PageID == Model.PageID).SingleOrDefault();
                _Page.Published = Model.Published;
                var _PagesC = _c.PageCultures.Where(m => m.PageID == Model.PageID).ToList();
                foreach (var item in _PagesC)
                {
                    item.Published = Model.Published;
                }
                _c.SaveChanges();
            }
        }
        public void InMenu(PublishBinding Model, string UserID)
        {
            using (var _c = db)
            {
                var _P = _c.Pages.Where(m => m.PageID == Model.PageID).SingleOrDefault();
                Can(_P.SiteID, UserID, _c);

                var _Page = _c.Pages.Where(m => m.PageID == Model.PageID).SingleOrDefault();
                _Page.InMenu = Model.Published;
                _c.SaveChanges();
            }
        }
    }
}
