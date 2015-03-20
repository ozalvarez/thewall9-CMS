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
        public int Save(CategoryBinding Model, string UserID)
        {
            using (var _c = db)
            {
                Can(Model.SiteID, UserID, _c);
                var _Category = new Category();
                if (Model.CategoryID == 0)
                {
                    //CREATING
                    var _Parent = _c.Categories.Where(m => m.CategoryParentID == Model.CategoryParentID).Select(m => m.Priority);
                    _Category.CategoryParentID = Model.CategoryParentID;
                    _Category.Priority = _Parent.Any() ? _Parent.Max() + 1 : 0;
                    _Category.SiteID = Model.SiteID;
                    _c.Categories.Add(_Category);
                }
                else
                    //UPDATING
                    _Category = _c.Categories.Where(m => m.CategoryID == Model.CategoryID).FirstOrDefault();
                _Category.CategoryAlias = Model.CategoryAlias;
                _c.SaveChanges();
                return _Category.CategoryID;
            }
        }

        public List<CategoryBinding> Get(int SiteID)
        {
            using (var _c = db)
            {
                var _Category = _c.Categories.Where(m => m.Site.SiteID == SiteID).ToList();
                return Get(_Category, 0, _c);
            }
        }
        private List<CategoryBinding> Get(List<Category> Model, int ParentID, ApplicationDbContext _c)
        {
            return (from c in Model
                    where c.CategoryParentID == ParentID
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
                        }),

                        CategoryItems = Model.Where(m => m.CategoryParentID == c.CategoryID).Any()
                        ? Get(Model.Where(m => m.CategoryParentID == c.CategoryID).ToList(), c.CategoryID, _c)
                        : new List<CategoryBinding>()
                    }).ToList();
        }
    }
}
