using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.bll.Utils;
using thewall9.data;
using thewall9.data.binding;
using thewall9.data.Models;

namespace thewall9.bll
{
    public class BrandBLL : BaseBLL
    {
        private Brand GetByID(int BrandID, ApplicationDbContext _c)
        {
            return _c.Brands.Where(m => m.BrandID == BrandID).SingleOrDefault();
        }
        public List<BrandBinding> Get(int SiteID)
        {
            using (var _c = db)
            {
                return _c.Brands.Where(m => m.SiteID == SiteID).Select(m => new BrandBinding
                {
                    BrandID = m.BrandID,
                    BrandName = m.BrandName,
                    BrandDescription = m.BrandDescription,
                    IconUrl = m.IconUrl,
                    SiteID = m.SiteID,
                    Icon=new FileReadBinding
                    {
                        FileUrl=m.IconUrl
                    }
                }).ToList();
            }
        }
        public void Save(BrandBinding Model, string UserID)
        {
            using (var _c = db)
            {
                Can(Model.SiteID, UserID, _c);
                var _Brand = new Brand(Model);
                if (Model.BrandID == 0)
                    _c.Brands.Add(_Brand);
                else
                {
                    _Brand = GetByID(Model.BrandID, _c);
                    _Brand.SetValues(Model);
                }
                _c.SaveChanges();
                _Brand.IconUrl = new FileReadBLL().AddImage(Model.Icon, FileReadBLL.FileType.BRAND, _Brand.BrandID.ToString(), _Brand.IconUrl, Model.SiteID);
                _c.SaveChanges();
            }
        }
        public void Delete(int BrandID, string UserID)
        {
            using (var _c = db)
            {
                var _Brand = GetByID(BrandID, _c);
                Can(_Brand.SiteID, UserID, _c);
                _c.Brands.Remove(_Brand);
                _c.SaveChanges();
                new FileReadBLL().Remove(FileReadBLL.FileType.BRAND, BrandID.ToString(), _Brand.SiteID);
            }
        }
    }
}
