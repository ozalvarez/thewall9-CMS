using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data;
using thewall9.data.binding;
using thewall9.data.Models;

namespace thewall9.bll
{
    public class CategoryBLL : BaseBLL
    {
        private Category GetByID(int CategoryID, ApplicationDbContext _c)
        {
            return _c.Categories.Where(m => m.CategoryID == CategoryID).SingleOrDefault();
        }
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
                            CultureName = m.Culture.Name
                        }).ToList(),

                        CategoryItems = Model.Where(m => m.CategoryParentID == c.CategoryID).Any()
                        ? GetTree(Model, c.CategoryID, _c)
                        : new List<CategoryBinding>()
                    }).ToList();
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
                    _Category.CategoryCultures=new List<CategoryCulture>();
                    //ADDING CULTURES
                    if (Model.CategoryCultures != null)
                    {
                        foreach (var item in Model.CategoryCultures)
                        {
                            _Category.CategoryCultures.Add(new CategoryCulture
                            {
                                CategoryName = item.CategoryName,
                                CultureID = item.CultureID
                            });
                        }
                    }
                    _c.Categories.Add(_Category);
                }
                else
                {
                    //UPDATING
                    _Category = _c.Categories.Where(m => m.CategoryID == Model.CategoryID).FirstOrDefault();
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
                                    CultureID = item.CultureID
                                });
                            }
                            else
                            {
                                var _CC = _Category.CategoryCultures.Where(m => m.CultureID == item.CultureID).SingleOrDefault();
                                _CC.CategoryName = item.CategoryName;
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
                var _P = _c.Categories.Where(m => m.CategoryParentID == _Category.CategoryParentID && m.SiteID==_Category.SiteID);
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
    }
}
