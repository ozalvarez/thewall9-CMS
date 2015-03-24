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
    public class TagBLL:BaseBLL
    {
        private Tag GetByID(int TagID, ApplicationDbContext _c)
        {
            return _c.Tags.Where(m => m.TagID == TagID).SingleOrDefault();
        }
        public List<TagBinding> Get(int SiteID)
        {
            using (var _c=db)
            {
                return _c.Tags.Where(m => m.SiteID == SiteID).Select(m=>new TagBinding
                {
                    TagID=m.TagID,
                    TagName=m.TagName,
                    SiteID=m.SiteID
                }).ToList();
            }
        }
        public void Save(TagBinding Model, string UserID)
        {
            using (var _c=db)
            {
                Can(Model.SiteID, UserID, _c);
                if (Model.TagID == 0)
                {
                    _c.Tags.Add(new data.Tag
                    {
                        TagName = Model.TagName,
                        SiteID = Model.SiteID
                    });
                }
                else
                {
                    var _Tag=GetByID(Model.TagID,_c);
                    _Tag.TagName = Model.TagName;
                }
                _c.SaveChanges();
            }
        }
        public void Delete(int TagID, string UserID)
        {
            using (var _c = db)
            {
                var _Tag = GetByID(TagID, _c);
                Can(_Tag.SiteID, UserID, _c);
                _c.Tags.Remove(_Tag);
                _c.SaveChanges();
            }
        }
    }
}
