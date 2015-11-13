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
    public class CultureBLL : BaseBLL
    {
        private Culture GetByID(int CultureID, ApplicationDbContext _c){
            return _c.Cultures.Where(m => m.CultureID == CultureID).SingleOrDefault();
        }
        public List<CultureBase> Get(int SiteID)
        {
            using (var _c = db)
            {
                return _c.Cultures.Where(m => m.SiteID == SiteID).Select(m => new CultureBase
                {
                    CultureID = m.CultureID,
                    Name = m.Name,
                    SiteID = m.SiteID,

                    Facebook=m.Facebook,
                    GPlus=m.GPlus,
                    Instagram=m.Instagram,
                    Tumblr=m.Tumblr,
                    Twitter=m.Twitter,
                    Rss = m.Rss,
                    YoutubeChannel = m.YoutubeChannel
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
                    FriendlyUrl = m.PageCulture.Where(m2 => m2.Page.Alias.Equals("home")).FirstOrDefault().FriendlyUrl,

                    Facebook = m.Facebook,
                    GPlus = m.GPlus,
                    Instagram = m.Instagram,
                    Tumblr = m.Tumblr,
                    Twitter = m.Twitter,
                    Rss=m.Rss,
                    YoutubeChannel=m.YoutubeChannel
                }).ToList();
            }
        }
        public Culture GetByName(int SiteID, string Name)
        {
            using (var _c = db)
            {
                return _c.Cultures.Where(m => m.SiteID == SiteID && m.Name.ToLower().Equals(Name)).FirstOrDefault();
            }
        }
        public void Save(CultureBinding Model, string UserID)
        {
            using (var _c=db)
            {
                Can(Model.SiteID, UserID, _c);
                var _Culture = GetByID(Model.CultureID, _c);
                _Culture.Facebook = Model.Facebook;
                _Culture.GPlus = Model.GPlus;
                _Culture.Instagram = Model.Instagram;
                _Culture.Tumblr = Model.Tumblr;
                _Culture.Twitter = Model.Twitter;
                _Culture.Rss = Model.Rss;
                _Culture.YoutubeChannel = Model.YoutubeChannel;
                _c.SaveChanges();
            }
        }
    }
}
