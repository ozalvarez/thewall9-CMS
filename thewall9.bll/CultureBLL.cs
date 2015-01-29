using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data;
using thewall9.data.binding;

namespace thewall9.bll
{
    public class CultureBLL : BaseBLL
    {
        public List<CultureBase> Get(int SiteID)
        {
            using (var _c = db)
            {
                return _c.Cultures.Where(m => m.SiteID == SiteID).Select(m => new CultureBase
                {
                    CultureID = m.CultureID,
                    Name = m.Name,
                    SiteID = m.SiteID
                }).ToList();
            }
        }
        public List<CultureRoutes> Get(int SiteID, string Url)
        {
            using (var _c = db)
            {
                var _Q = SiteID != 0
                ? from c in _c.Cultures
                  where c.SiteID == SiteID
                  select c
                       : from c in _c.Cultures
                         join u in _c.SiteUrls on c.SiteID equals u.SiteID
                         where u.Url.Contains(Url)
                         select c;
                return _Q.Select(m => new CultureRoutes
                {
                    CultureID = m.CultureID,
                    Name = m.Name,
                    SiteID = m.SiteID,
                    FriendlyUrl = m.PageCulture.Where(m2 => m2.Page.Alias.Equals("home")).FirstOrDefault().FriendlyUrl
                }).ToList();
            }
        }
    }
}
