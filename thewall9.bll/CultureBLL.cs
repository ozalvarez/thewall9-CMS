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
    }
}
