using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.bll.Exceptions;
using thewall9.data;
using thewall9.data.binding;
using thewall9.data.Models;
using thewall9.bll.Utils;
namespace thewall9.bll
{
    public class CategoryBLL : BaseBLL
    {

        #region Web
        public List<CategoryWeb> Get(int SiteID, string Url, string Lang)
        {
            using (var _c = db)
            {
                var _Q = SiteID != 0
                    ? from m in _c.CategoryCultures
                      where m.Category.SiteID == SiteID && m.Culture.Name.ToLower().Equals(Lang.ToLower())
                      select m
                     : from m in _c.CategoryCultures
                       join u in _c.SiteUrls on m.Category.SiteID equals u.SiteID
                       where u.Url.Equals(Url) && m.Culture.Name.ToLower().Equals(Lang.ToLower())
                       select m;

                var _Category = _Q.ToList();
                return GetTree(_Category, 0, null, _c);
            }
        }
        private List<CategoryWeb> GetTree(List<CategoryCulture> Model, int ParentID, String ParentName, ApplicationDbContext _c)
        {
            return (from c in Model
                    where c.Category.CategoryParentID == ParentID
                    orderby c.Category.Priority
                    select new CategoryWeb
                    {
                        CategoryName = c.CategoryName,
                        FriendlyUrl = c.FriendlyUrl,
                        CategoryParentName=ParentName,
                        CategoryItems = Model.Where(m => m.Category.CategoryParentID == c.CategoryID).Any()
                        ? GetTree(Model, c.CategoryID, c.CategoryName, _c)
                        : new List<CategoryWeb>()
                    }).ToList();
        }
        #endregion

        #region Customer
        public List<CategoryBinding> Get(int SiteID, string UserID)
        {
            using (var _c = db)
            {
                Can(SiteID, UserID, _c);
                var _Category = _c.Categories.Where(m => m.Site.SiteID == SiteID).ToList();
                return GetTree(_Category, 0, _c);
            }
        }
        private List<CategoryBinding> GetTree(List<Category> Model, int ParentID, ApplicationDbContext _c)
        {
            return (from c in Model
                    where c.CategoryParentID == ParentID
                    orderby c.Priority
                    select new CategoryBinding
                    {
                        CategoryAlias = c.CategoryAlias,
                        CategoryID = c.CategoryID,
                        CategoryParentID = c.CategoryParentID,
                        Priority = c.Priority,
                        SiteID = c.SiteID,

                        CategoryCultures = c.CategoryCultures.Select(m => new CategoryCultureBinding
                        {
                            CategoryName = m.CategoryName,
                            CultureID = m.CultureID,
                            CultureName = m.Culture.Name,
                            FriendlyUrl = m.FriendlyUrl
                        }).ToList(),

                        CategoryItems = Model.Where(m => m.CategoryParentID == c.CategoryID).Any()
                        ? GetTree(Model, c.CategoryID, _c)
                        : new List<CategoryBinding>()
                    }).ToList();
        }
        private Category GetByID(int CategoryID, ApplicationDbContext _c)
        {
            return _c.Categories.Where(m => m.CategoryID == CategoryID).SingleOrDefault();
        }
        private string GetFriendlyUrl(CategoryBinding Model, string CategoryName, string FriendlyUrl, int CultureID, ApplicationDbContext _c)
        {
            if (!string.IsNullOrEmpty(FriendlyUrl))
            {
                var _G = Model.CategoryCultures.GroupBy(m => m.FriendlyUrl);
                if (_G.Count() < Model.CategoryCultures.Count())
                    throw new RuleException("FriendlyURL Should be Different", "0x002");
            }

            FriendlyUrl = string.IsNullOrEmpty(FriendlyUrl) ? CategoryName.CleanUrl() : FriendlyUrl;
            if (Model.CategoryID != 0)
            {
                if (_c.CategoryCultures.Where(m => m.Category.SiteID == Model.SiteID
                                    && m.FriendlyUrl == FriendlyUrl
                                    && m.CategoryID != Model.CategoryID
                                    && m.CultureID != CultureID).Any())
                    throw new RuleException("FriendlyURL Exist", "0x001");
            }
            else
            {
                if (_c.CategoryCultures.Where(m => m.Category.SiteID == Model.SiteID
                                && m.FriendlyUrl == FriendlyUrl).Any())
                    throw new RuleException("FriendlyURL Exist", "0x001");
            };
            return FriendlyUrl;
        }
        public int Save(CategoryBinding Model, string UserID)
        {
            using (var _c = db)
            {
                Can(Model.SiteID, UserID, _c);
                var _Category = new Category();
                var _NewBros = _c.Categories.Where(m => m.CategoryParentID == Model.CategoryParentID && m.SiteID == Model.SiteID);
                if (Model.CategoryID == 0)
                {
                    //CREATING
                    _Category.SiteID = Model.SiteID;
                    _Category.Priority = _NewBros.Select(m => m.Priority).Any() ? _NewBros.Select(m => m.Priority).Max() + 1 : 0;
                    _Category.CategoryCultures = new List<CategoryCulture>();
                    //ADDING CULTURES
                    if (Model.CategoryCultures != null)
                    {
                        foreach (var item in Model.CategoryCultures)
                        {
                            _Category.CategoryCultures.Add(new CategoryCulture
                            {
                                CategoryName = item.CategoryName,
                                CultureID = item.CultureID,
                                FriendlyUrl = GetFriendlyUrl(Model, item.CategoryName, item.FriendlyUrl, item.CultureID, _c)
                            });
                        }
                    }
                    _c.Categories.Add(_Category);
                }
                else
                {
                    //UPDATING
                    _Category = GetByID(Model.CategoryID, _c);
                    //ADDING CULTURES
                    if (Model.CategoryCultures != null)
                    {
                        foreach (var item in Model.CategoryCultures)
                        {
                            if (item.Adding)
                            {
                                _Category.CategoryCultures.Add(new CategoryCulture
                                {
                                    CategoryName = item.CategoryName,
                                    CultureID = item.CultureID,
                                    FriendlyUrl = GetFriendlyUrl(Model, item.CategoryName, item.FriendlyUrl, item.CultureID, _c)
                                });
                            }
                            else
                            {
                                var _CC = _Category.CategoryCultures.Where(m => m.CultureID == item.CultureID).SingleOrDefault();
                                _CC.CategoryName = item.CategoryName;
                                _CC.FriendlyUrl = GetFriendlyUrl(Model, item.CategoryName, item.FriendlyUrl, item.CultureID, _c);
                            }
                        }
                    }
                    if (_Category.CategoryParentID != Model.CategoryParentID)
                    {
                        _Category.Priority = _NewBros.Select(m => m.Priority).Any() ? _NewBros.Select(m => m.Priority).Max() + 1 : 0;
                        //UPDATE PRIORITIES
                        var _Bros = _c.Categories.Where(m => m.CategoryParentID == _Category.CategoryParentID && m.SiteID == _Category.SiteID && m.Priority > _Category.Priority).ToList();
                        foreach (var item in _Bros)
                        {
                            item.Priority--;
                        }
                    }

                }
                _Category.CategoryAlias = Model.CategoryAlias;
                _Category.CategoryParentID = Model.CategoryParentID;
                _c.SaveChanges();
                return _Category.CategoryID;
            }
        }
        public void UpOrDown(UpOrDown Model, string UserID)
        {
            using (var _c = db)
            {
                var _Category = GetByID(Model.CategoryID, _c);
                Can(_Category.SiteID, UserID, _c);
                var _P = _c.Categories.Where(m => m.CategoryParentID == _Category.CategoryParentID && m.SiteID == _Category.SiteID);
                if (Model.Up)
                {
                    if (_P.Select(m => m.Priority).Min() < _Category.Priority)
                    {
                        var _Next = _P.Where(m => m.Priority < _Category.Priority).OrderBy(m => m.Priority).ToList().Last();
                        _Next.Priority++;
                        _Category.Priority--;
                    }
                }
                else
                {
                    if (_P.Select(m => m.Priority).Max() > _Category.Priority)
                    {
                        var _Next = _P.Where(m => m.Priority > _Category.Priority).OrderBy(m => m.Priority).ToList().First();
                        _Next.Priority--;
                        _Category.Priority++;
                    }
                }
                _c.SaveChanges();
            }
        }
        public void Delete(int CategoryID, string UserID)
        {
            using (var _c = db)
            {
                var _Childs = _c.Categories.Where(m => m.CategoryParentID == CategoryID).ToList();
                foreach (var item in _Childs)
                {
                    Delete(item.CategoryID, UserID);
                }
                var _Category = GetByID(CategoryID, _c);
                var _Bro = _c.Categories.Where(m => m.CategoryParentID == _Category.CategoryParentID && m.Priority > _Category.Priority).OrderBy(m => m.Priority).ToList();
                foreach (var item in _Bro)
                {
                    item.Priority--;
                }
                _c.Categories.Remove(_Category);
                _c.SaveChanges();
            }
        }
        #endregion
    }
}
